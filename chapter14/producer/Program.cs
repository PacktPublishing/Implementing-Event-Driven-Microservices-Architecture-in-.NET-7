
using Confluent.Kafka;
using Microsoft.ApplicationInsights;
using Swashbuckle.AspNetCore.SwaggerGen;
using producer;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var producerConfig = new ProducerConfig();

var ProducerTopic = builder.Configuration.GetValue<string>("Topic");
builder.Configuration.Bind("ProducerConfig", producerConfig);
builder.Services.AddSingleton<ProducerConfig>(producerConfig);
builder.Services.AddSingleton(ProducerTopic);
builder.Services.AddScoped<IProducerService, producerService>();
builder.Services.AddApplicationInsightsTelemetry(
    (options) =>
    options.ConnectionString =
    "--- insert your app insights connection string ---"
);
builder.Services.AddApplicationInsightsKubernetesEnricher();

builder.Services.AddControllers();
builder.Services.AddMvcCore().AddApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var telemetryClient = scope.ServiceProvider.GetRequiredService<TelemetryClient>();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/healthz", () => "Ok!");

app.MapGet("/complexhealthz", (HttpContext http) =>
{
    if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
        http.Response.StatusCode = 500;
    else
        http.Response.StatusCode = 200;
    return $"It's complicated! {http.Response.StatusCode}";
});

var appStartTime = DateTime.UtcNow;
app.MapGet("/readyz", (HttpContext http) =>
{
    var readyAt = appStartTime.AddSeconds(30);
    if (DateTime.UtcNow < readyAt)
    {
        http.Response.StatusCode = 500;
        return $"Ready in {(readyAt - DateTime.UtcNow).TotalSeconds} seconds. {http.Response.StatusCode}";
    }
    else
    {
        http.Response.StatusCode = 200;
        return $"Ready! {http.Response.StatusCode}";
    }
});

app.MapPost("/send", async (HttpContext http) =>
{
    var causationId = System.Diagnostics.Activity.Current.TraceId.ToString();
    var correlationId = http.Request.Headers.RequestId.FirstOrDefault() ?? causationId;
    telemetryClient.Context.Operation.Id = correlationId;

    var message = "";
    using (StreamReader sr = new StreamReader(http.Request.Body))
    {
        message = await sr.ReadToEndAsync();
    }

    var svc = http.RequestServices.GetRequiredService<IProducerService>();
    await svc.SetTopic(ProducerTopic);
    logger.LogDebug("Producer service set the topic to {topic}", ProducerTopic);
    try
    {
        if (message == null)
        {
            throw new InvalidDataException("Must have a message body");
        }
        await svc.Send(message, correlationId, causationId);
    }
    catch (Exception ex)
    {
        await http.Response.BodyWriter.WriteAsync(System.Text.Encoding.ASCII.GetBytes(ex.Message));
        http.Response.StatusCode = 500;
    }
    http.Response.StatusCode = 200;
});

await app.RunAsync();

