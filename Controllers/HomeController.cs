using EcdsApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

            ViewBag.LayerInfo = await _context.ThemeLayerDetails
                .Include(t => t.SubThemes.Themes)
                .Include(t => t.ThemeLayerTypes)
                .Include(t => t.BoundaryInfo)
                .Include(t => t.TableInfo).OrderByDescending(t=>t.LayerId).Take(5).ToListAsync();

            return View();
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
