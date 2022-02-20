using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTAEDA.Equipment.Query.API.Controllers
{
    public class EquipmentController : BaseQueryController
    {
        public IActionResult Index()
        {

            return View();
        }
    }

  
}
