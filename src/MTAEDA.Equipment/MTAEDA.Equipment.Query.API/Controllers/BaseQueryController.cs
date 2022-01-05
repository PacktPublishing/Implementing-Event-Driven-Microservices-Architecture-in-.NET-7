using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Query.API.Controllers
{
    public class BaseQueryController : Controller
    {
        public List<IServiceProvider> Consumers { get; private set; } = new List<IServiceProvider>();   

        public BaseQueryController()
        {
            // grab services

        }
        public async Task<IActionResult> Up()
        {
            return await Task.Run(() => { return new ContentResult() { Content = "up", StatusCode = 200 }; });
        }

        public async Task<IActionResult> Health()
        {
            // TODO: Add logic here to determine if the endpoint is healthy or not
            return await Task.Run(() => { return new ContentResult() { Content = "up", StatusCode = 200 }; });
        }

    }

    
}
