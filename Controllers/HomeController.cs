using EcdsApp.Data;
using EcdsApp.Models.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var exposureList = _context.UpazilaWiseExposureData
                .Include(u => u.Upazila)
                .OrderBy(u => u.Id).Take(12)
                .ToList();

            var layerInfoList = _context.ThemeLayerDetails
                .Include(t => t.SubThemes.Themes)
                .Include(t => t.ThemeLayerTypes)
                .Include(t => t.BoundaryInfo)
                .Include(t => t.TableInfo)
                .OrderByDescending(t => t.LayerId).Take(5)
                .ToList();

            var dashboardModel = new DashboardVm
            {
                UpazilaWiseExposureData = exposureList, 
                ThemeLayerDetails = layerInfoList
            };
            return View(dashboardModel);
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
