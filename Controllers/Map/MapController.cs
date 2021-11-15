using EcdsApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Map
{
    public class MapController : Controller
    {
        private readonly DataContext _context;

        public MapController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> BasicMap()
        {
            var dataContext = _context.ThemeLayerDetails.Include(s => s.SubThemes.Themes);
            ViewBag.LayerInfoes=await dataContext.ToListAsync();

            return View();
        }
        public IActionResult ThematicMap()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLayerInformation(int layer_id)
        {
            var data = _context.ThemeLayerDetails.Include(s => s.SubThemes.Themes)
                .Where(s => s.LayerId == layer_id)
                .Select(sd => new
                {
                    path = (sd.SubThemes.Themes.ThemePath + "/" + sd.SubThemes.SubThemePath + "/" + sd.LayerPath + "/" + sd.LayerFileName),
                    mainAtt = sd.LayerMainAttribureName,
                }).FirstOrDefault();

            return Json(data);
        }
    }
}
