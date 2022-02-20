using CloudNative.CloudEvents;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using MTAEDA.Core.Interfaces;
using MTAEDA.Core.Infrastructure;
using MTAEDA.Core.Infrastructure.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MTAEDA.Equipment.API.Controllers
{
    public class BaseCommandController: Controller
    {
        public ProducerConfig Config { get; private set;  }
        public ILogger<BaseCommandController> Logger { get; private set;  }

        public BaseCommandController(ProducerConfig config, ILogger<BaseCommandController> logger)
        {
            Config = config;   
            Logger = logger;
        }

        [HttpGet("up")]
        public async Task<IActionResult> Up()
        {
            return await Task.Run(() => { return new ContentResult() { Content = "up", StatusCode = 200 }; });
        }

        [HttpGet("health")]
        public async Task<IActionResult> Health()
        {
            // TODO: Add logic here to determine if the endpoint is healthy or not
            return await Task.Run(() => { return new ContentResult() { Content = "health check OK", StatusCode = 200 }; });
        }
        protected async Task<IActionResult> RaiseEvent(IDomainEvent evt)
        {
            /*var qs = HttpUtility.ParseQueryString(Request.QueryString.Value);
            if (qs["dest"] is null || string.IsNullOrEmpty(qs["dest"]))
            {
                Logger.LogError("Cannot send a message to a topic with no name.", evt);
                return new ContentResult() { Content = JsonSerializer.Serialize(new ProduceException<string, string>(new Error(ErrorCode.InvalidConfig, "No topic name provided."), null)), StatusCode = 500 };
            }*/
            IEventWriterProvider provider = new KafkaProducer("equipment", Config, Logger);
            evt.DomainData = evt.GetType().ToString();
            var cloudEvt = new CloudEvent()
            {
                Source = new System.Uri($"{Request.Scheme}://{Request.Host}/api/turnstile/event"),
                Data = evt,
                Time = System.DateTimeOffset.Now,
                Type = "DomainEvent",
                Subject = "Turnstile locked"
            };

            await provider.Send(cloudEvt);
            return new ContentResult() { Content = "Message sent", StatusCode = 200 };
        }
    }
}
