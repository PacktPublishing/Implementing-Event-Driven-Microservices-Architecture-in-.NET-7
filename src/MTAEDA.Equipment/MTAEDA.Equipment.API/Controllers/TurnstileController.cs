using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MTAEDA.Equipment.API.Controllers;
using MTAEDA.Equipment.Domain.Events;
using System.Threading.Tasks;

[Route("/api/[controller]")]
public class TurnstileController : BaseCommandController
{
    public TurnstileController(ProducerConfig config, ILogger<TurnstileController> logger) : base(config, logger)
    {
        
    } 


    [HttpPost("lock")]
    public async Task<IActionResult> Lock(int turnstileId)
    {
        return await RaiseEvent(TurnstileLockedEvent.Create(turnstileId,System.DateTime.Now));
    }

    [HttpPost("tsinstall")]
    public async Task<IActionResult> NotifyInstall(int turnstileId, int stationId)
    {
        return await RaiseEvent(TurnstileInstalledEvent.Create(turnstileId,stationId, System.DateTime.Now));
    }

    
   
}
