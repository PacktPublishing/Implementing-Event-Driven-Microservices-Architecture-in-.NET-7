
using Confluent.Kafka;
using producer;

var builder = WebApplication.CreateBuilder(args);
var producerConfig = new ProducerConfig();

var ProducerTopic = builder.Configuration.GetValue<string>("Topic");
builder.Configuration.Bind("ProducerConfig", producerConfig);
builder.Services.AddSingleton<ProducerConfig>(producerConfig);
builder.Services.AddSingleton(ProducerTopic);
builder.Services.AddScoped<IProducerService, producerService>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");
app.MapPost("/send", async (http) => {
    var message = "";
    using (StreamReader sr = new StreamReader(http.Request.Body))
    {
        message = await sr.ReadToEndAsync();
    }

    var svc = http.RequestServices.GetRequiredService<IProducerService>();
    await svc.SetTopic(ProducerTopic);
    try
    {
        if (message == null)
        {
            throw new InvalidDataException("Must have a message body");
        }
        await svc.Send(message?.ToString());
    }
    catch (Exception ex)
    {
        await http.Response.BodyWriter.WriteAsync(System.Text.Encoding.ASCII.GetBytes(ex.Message));
        http.Response.StatusCode = 500;
    }
    http.Response.StatusCode = 200;
});

await app.RunAsync();

