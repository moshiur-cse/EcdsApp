using EcdsApp.Data;
using EcdsApp.Models;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Region
{
    [Authorize]
    public class AdminBoundaryUpazilasController : Controller
    {
        private readonly DataContext _context;

        public AdminBoundaryUpazilasController(DataContext context)
        {
            _context = context;
        }

        // GET: AdminBoundaryUpazilas
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.AdminBoundaryUpazilas.Include(a => a.District);
            return View(await dataContext.ToListAsync());
        }

        public async Task<IActionResult> SummaryData(string adminCode, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.DivisionCode = adminCode;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryUpazilas.Include(a => a.District).Where(i => i.UpazilaGeoCode == adminCode).ToListAsync());
        }

        public async Task<IActionResult> DetailsData(int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryUpazilas.Include(a => a.District).ToListAsync());
        }

        // GET: AdminBoundaryUpazilas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryUpazila = await _context.AdminBoundaryUpazilas
                .Include(a => a.District)
                .FirstOrDefaultAsync(m => m.UpazilaGeoCode == id);
            if (adminBoundaryUpazila == null)
            {
                return NotFound();
            }

            return View(adminBoundaryUpazila);
        }

        // GET: AdminBoundaryUpazilas/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            ViewData["DistrictName"] = new SelectList(_context.AdminBoundaryDistricts, "DistrictGeoCode", "DistrictName");
            return View();
        }

        // POST: AdminBoundaryUpazilas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("UpazilaGeoCode,UpazilaName,UpazilaNameBangla,DistrictGeoCode,SortingOrder")] AdminBoundaryUpazila adminBoundaryUpazila)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminBoundaryUpazila);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictGeoCode"] = new SelectList(_context.AdminBoundaryDistricts, "DistrictGeoCode", "DistrictGeoCode", adminBoundaryUpazila.DistrictGeoCode);
            return View(adminBoundaryUpazila);
        }

        // GET: AdminBoundaryUpazilas/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryUpazila = await _context.AdminBoundaryUpazilas.FindAsync(id);
            if (adminBoundaryUpazila == null)
            {
                return NotFound();
            }
            ViewData["DistrictName"] = new SelectList(_context.AdminBoundaryDistricts, "DistrictGeoCode", "DistrictName", adminBoundaryUpazila.DistrictGeoCode);
            return View(adminBoundaryUpazila);
        }

        // POST: AdminBoundaryUpazilas/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(AdminBoundaryUpazila adminBoundaryUpazila, string districtName)
        {
            AdminBoundaryDistrict item = await _context.AdminBoundaryDistricts.Where(x => x.DistrictName == districtName).FirstOrDefaultAsync();
            adminBoundaryUpazila.DistrictGeoCode = item.DistrictGeoCode;
            adminBoundaryUpazila.District= item;

            if (ModelState.IsValid)
            {

                try
                {
                    //_context.Update(adminBoundaryUpazila);
                    //await _context.SaveChangesAsync();
                    return Json("success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminBoundaryUpazilaExists(adminBoundaryUpazila.UpazilaGeoCode))
                    {
                        return Json("Failed to Save.");
                    }

                    throw;
                }

                
            }

            //===displaying all model errors.

            var errormsg = "";
            foreach (var modelState in ModelState.Values)
            {
                foreach (var modelError in modelState.Errors)
                {
                    errormsg += modelError.ErrorMessage + " ";
                }
            }

            return Json(errormsg);

        }

        // GET: AdminBoundaryUpazilas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryUpazila = await _context.AdminBoundaryUpazilas
                .Include(a => a.District)
                .FirstOrDefaultAsync(m => m.UpazilaGeoCode == id);
            if (adminBoundaryUpazila == null)
            {
                return NotFound();
            }

            return View(adminBoundaryUpazila);
        }

        // POST: AdminBoundaryUpazilas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminBoundaryUpazila = await _context.AdminBoundaryUpazilas.FindAsync(id);
            _context.AdminBoundaryUpazilas.Remove(adminBoundaryUpazila);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminBoundaryUpazilaExists(string id)
        {
            return _context.AdminBoundaryUpazilas.Any(e => e.UpazilaGeoCode == id);
        }
    }
}
