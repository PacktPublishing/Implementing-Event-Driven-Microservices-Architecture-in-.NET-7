using MTAEDA.Core.Domain.EventHandlers;
using MTAEDA.Core.Domain.Events;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/failback", (context) =>
{
    return Task.Run(() =>
    {
        HttpStatusCode returnCode = HttpStatusCode.OK;
        HttpContent returnContent;

        RetryFailbackEvent evt = RetryFailbackEvent.Create("http://failover.site", "equipment", "turnstileIncrement");
        using (StreamReader sr = new StreamReader(context.Request.Body))
        {
            try
            {
                evt = JsonConvert.DeserializeObject<RetryFailbackEvent>(sr.ReadToEnd()) ?? evt;
                RetryFailbackEventHandler handler = new RetryFailbackEventHandler();
                handler.Handle(evt, CancellationToken.None);
                returnContent = JsonContent.Create(new { message = "Operation Successful" });
            }
            catch (Exception ex)
            {
                returnContent = JsonContent.Create(ex);
            }

        }
        return new HttpResponseMessage(returnCode) { Content = returnContent };
    });
});


app.Run();


