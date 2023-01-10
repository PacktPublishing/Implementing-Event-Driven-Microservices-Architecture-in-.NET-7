using CloudNative.CloudEvents;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using MTAEDA.Core.Interfaces;
using MTAEDA.Core.Infrastructure;
using MTAEDA.Core.Infrastructure.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using Polly.Retry;
using MTAEDA.Core.Utility;

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
        protected async Task<IActionResult> RaiseEvent(IDomainEvent evt, string eventSubject)
        {
            string returnMessage = "Message sent";
            int statusCode = 200;
            try
            {
                IEventWriterProvider provider = new KafkaProducer("equipment", Config, Logger);
                evt.DomainData = evt.GetType().ToString();
                var cloudEvt = await EventUtil.Pack(evt, new System.Uri($"{Request.Scheme}://{Request.Host}/api/turnstile/event"), CloudEventType.DomainEvent, eventSubject);

                await provider.Send(cloudEvt);
                Logger.LogTrace(cloudEvt.Id, returnMessage);
                return new ContentResult() { Content = returnMessage, StatusCode = statusCode };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                returnMessage = $"An error was encountered: {ex.Message}";
                statusCode = 500;
                throw ex;
            }
            
            

        }
    }
}
