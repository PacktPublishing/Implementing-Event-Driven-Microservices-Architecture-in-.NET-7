using Microsoft.AspNetCore.Mvc;

namespace producer
{
    [Route("[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public class ProducerController : ControllerBase
    {
        private readonly HttpContext httpcontext;
        public ProducerController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpcontext = httpContextAccessor.HttpContext;

        }

        [HttpPost("Send")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Send([FromBody] string message)
        {
            var causationId = System.Diagnostics.Activity.Current.TraceId.ToString();
            var correlationId = Request.Headers.RequestId.FirstOrDefault() ?? causationId;
            var svc = httpcontext.RequestServices.GetRequiredService<IProducerService>();
            await svc.SetTopic("equipment");
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
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkObjectResult("Ok!");
        }
    }
}
