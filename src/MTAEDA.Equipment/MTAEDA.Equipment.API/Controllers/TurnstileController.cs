using CloudNative.CloudEvents;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTAEDA.Core.Domain.Events;
using MTAEDA.Core.Utility;
using MTAEDA.Equipment.API.Controllers;
using MTAEDA.Equipment.Domain.Aggregates;
using MTAEDA.Equipment.Domain.Events;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Polly.Wrap;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

[Route("/api/[controller]")]
public class TurnstileController : BaseCommandController
{
    protected AsyncPolicy asyncPolicy;
    readonly int retries = 3;
    public TurnstileController(ProducerConfig config, ILogger<TurnstileController> logger) : base(config, logger)
    {
        asyncPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(retries, retryCount =>
        {
            return System.TimeSpan.FromSeconds(retryCount++);
        }, (result, ts, retryNumber, context) =>
        {
            if(retryNumber == retries)
            {
                logger.LogError($"{context.PolicyKey} at {context.OperationKey}: fallback sequence initiated, due to: {result.Message}");
                // Write out the information locally and attempt to send the information to the station server
            }
        });
        
        
    } 


    [HttpPost("lock")]
    public async Task<IActionResult> Lock(int turnstileId)
    {
        return await RaiseEvent(TurnstileLockedEvent.Create(turnstileId,System.DateTime.Now), "Turnstile locked");
    }

    [HttpPost("tsinstall")]
    public async Task<IActionResult> NotifyInstall(int turnstileId, int stationId)
    {
        return await RaiseEvent(TurnstileInstalledEvent.Create(turnstileId,stationId, System.DateTime.Now),"Turnstile installed");
    }

    [HttpPost("enter")]
    public async Task<IActionResult> MarkPassengerIngress(int turnstileId)
    {
        var evt = PassengerIngressEvent.Create(turnstileId);
        var result =  await asyncPolicy.ExecuteAndCaptureAsync(async () =>
        {
            return await Task.Run(async () =>
            {
                return await RaiseEvent(evt,"Passenger entry to station");
            });

        });
        
        if(result.Outcome == OutcomeType.Failure)
        {
            var item = RetryFailbackEvent.Create("", "equipment", "MarkPassengerIngress");
            item.DomainData = JsonConvert.SerializeObject(evt);
            return new ContentResult() { StatusCode = 500, Content = $"Error: {result.FinalException.Message}" };
        }
        return result.Result;

    }

    [HttpPost("exit")]
    public async Task<IActionResult> MarkPassengerEgress(int turnstileId)
    {
        var evt = PassengerEgressEvent.Create(turnstileId);
        var result = await asyncPolicy.ExecuteAndCaptureAsync(async () =>
        {
            return await Task.Run(async () =>
            {
                return await RaiseEvent(PassengerEgressEvent.Create(turnstileId),"Passenger exit from station");
            });
            
        });

        if (result.Outcome == OutcomeType.Failure)
        {
            var item = RetryFailbackEvent.Create("", "equipment", "MarkPassengerIngress");
            item.DomainData = JsonConvert.SerializeObject(evt);
            return new ContentResult() { StatusCode = 500, Content = $"Error: {result.FinalException.Message}" };
        }
        return result.Result;
    }
}
