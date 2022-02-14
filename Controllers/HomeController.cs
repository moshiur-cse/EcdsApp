using EcdsApp.Data;
using EcdsApp.Models.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Dashboard()
        {
            var population = _context.DistrictWisePopulations
                .Include(u => u.District.Division)
                .OrderBy(u => u.Id).Take(10)
                .ToList();

            var layerInfoList = _context.ThemeLayerDetails
                .Include(t => t.SubThemes.Themes)
                .Include(t => t.ThemeLayerTypes)
                .Include(t => t.BoundaryInfo)
                .Include(t => t.TableInfo)
                .OrderByDescending(t => t.LayerId).Take(5)
                .ToList();
            var layerData = await _context.DistrictWisePoverties.ToListAsync();
            var legendData=await _context.LayerLegendColors.Where(i=>i.LayerId==11).ToListAsync();
            ViewBag.DistrictList = new SelectList(_context.AdminBoundaryDistricts.OrderBy(i => i.DistrictName), "DistrictGeoCode", "DistrictName");

            var dashboardModel = new DashboardVm
            {
                DistrictWisePopulations = population, 
                ThemeLayerDetails = layerInfoList,
                DistrictWisePoverties= layerData,
                LayerLegendColors= legendData
            };
            return View(dashboardModel);
        }


         public IActionResult index()
        {
            return View();
        }
        public JsonResult GetChartData(List<string> distCodeList)
        {

            var data = _context.DistrictWisePopulations.Include(d => d.District).Where(i => distCodeList.Contains(i.DistrictGeoCode)).
                Select(j => new
                {
                    distName = j.District.DistrictName,
                    male = j.Male,
                    female = j.Female,
                    total = j.Total
                }).ToList();
               
            return Json(data);
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}
        //public IActionResult Register()
        //{
        //    return View();
        //}
    }
}
