using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Map
{
    public class MapController : Controller
    {
        public IActionResult BasicMap()
        {
            return View();
        }
        public IActionResult ThematicMap()
        {
            return View();
        }
    }
}
