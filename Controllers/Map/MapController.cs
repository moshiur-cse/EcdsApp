using EcdsApp.Data;
using EcdsApp.Models.ViewModels.Map;
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
            
            var themeList = _context.ThemeLayerDetails
                .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                .Select(k => new ThemeList
                {
                    themeName = k.Key,
                    subThemeList=k.GroupBy(i => i.SubThemes.SubThemeName)
                        .Select(j => new SubThemeList
                        {
                            subThemeName = j.Key,                           
                            layerPathList = j.Select(j=>j.LayerPath).ToList(),
                            layerIdList= j.Select(j => j.LayerId).ToList(),
                            layerNameList= j.Select(j => j.LayerName).ToList(),

                        }).ToList()


                }).ToList();
           
            return Json(themeList);

            ViewBag.LayerInfoes = await dataContext.ToListAsync();


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
                        
                        layerId=sd.LayerId,
                        layerName=sd.LayerName,
                        themePath = sd.SubThemes.Themes.ThemePath,
                        subThemePath = sd.SubThemes.SubThemePath,
                        layerPath = sd.LayerPath,
                        layerFileName = sd.LayerFileName,
                       
                        mainAttDisName = sd.MainAttributeDisplayName,
                        mainAtt = sd.MainAttributeName,
                        mainAttCode=sd.MainAttributeCode,

                        firstAttDisName = sd.FirstAttributeDisplayName,
                        firstAtt = sd.FirstAttributeName,
                        firstAttCode = sd.FirstAttributeCode,

                        secondAttDisName = sd.SecondAttributeDisplayName,
                        secondAtt = sd.SecondAttributeName,
                        secondAttCode = sd.SecondAttributeCode,

                        thirdAttDisName = sd.ThirdAttributeDisplayName,
                        thirdAtt = sd.ThirdAttributeName,
                        thirdAttCode = sd.ThirdAttributeCode,

                        cLat = sd.FileLatName,
                        cLong = sd.FileLongName,

                        layerTypeId = sd.LayerTypeId,
                        isLegendColor=sd.IsLegendColor,
                        legendcolorField=sd.LegendColorFieldName

                    }).FirstOrDefault();
                return Json(data);

        }

        
    }
}
