using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using CloudNative.CloudEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using MTAEDA.Core.Domain.EventHandlers;
using MTAEDA.Core.Domain.Events;
using MTAEDA.Core.Infrastructure;
using MTAEDA.Core.Infrastructure.Interfaces;
using MTAEDA.Equipment.Domain.Events;
using MTAEDA.Equipment.Infrastructure;
using MTAEDA.Equipment.RetryFailback;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ILogger>(sp =>
{
    return LoggerFactory.Create(c =>
    {
        c.AddConsole();
    }).CreateLogger("console");
});
builder.Services.AddSingleton(typeof(IotHubConnectivityWorker));
builder.Services.AddScoped<IEventWriterProvider>(sp =>
{
    return new EventHubDataWriter(builder.Configuration.GetConnectionString("PrimaryIoTHub"));
});
builder.Services.AddSingleton<EventHubProducerClient>(sp =>
{
    return new EventHubProducerClient(new EventHubConnection(builder.Configuration.GetConnectionString("PrimaryIoTHub")));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/up", () =>
{
    return new HttpResponseMessage(HttpStatusCode.OK);
}).WithOpenApi();

app.MapPost("/failback", ([FromBody] RetryFailbackEvent eventBody) =>
{
    return Task.Run(() =>
    {
        HttpStatusCode returnCode = HttpStatusCode.OK;
        HttpContent returnContent;

        try
        {
            RetryFailbackEventHandler handler = new RetryFailbackEventHandler();
            handler.Handle(eventBody, CancellationToken.None);
            returnContent = JsonContent.Create(new { message = "Operation Successful" });
        }
        catch (Exception ex)
        {
            returnContent = JsonContent.Create(ex);
        }


        return new HttpResponseMessage(returnCode) { Content = returnContent };
    });
}).WithOpenApi();

app.Services.GetService<IotHubConnectivityWorker>().RunWorkerAsync(Environment.ProcessPath);
app.Run();


