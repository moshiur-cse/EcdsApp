using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models;

namespace EcdsApp.Controllers.Region
{
    public class AdminBoundaryDistrictsController : Controller
    {
        private readonly DataContext _context;

        public AdminBoundaryDistrictsController(DataContext context)
        {
            _context = context;
        }

        // GET: AdminBoundaryDistricts
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.AdminBoundaryDistricts.Include(a => a.Division);
            return View(await dataContext.ToListAsync());
        }
        public async Task<IActionResult> SummaryData(string adminCode, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.DivisionCode = adminCode;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryDistricts.Include(a => a.Division).Where(i => i.DistrictGeoCode == adminCode).ToListAsync());
        }

        public async Task<IActionResult> DetailsData(int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryDistricts.Include(a => a.Division).ToListAsync());
        }
        // GET: AdminBoundaryDistricts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDistrict = await _context.AdminBoundaryDistricts
                .Include(a => a.Division)
                .FirstOrDefaultAsync(m => m.DistrictGeoCode == id);
            if (adminBoundaryDistrict == null)
            {
                return NotFound();
            }

            return View(adminBoundaryDistrict);
        }

        // GET: AdminBoundaryDistricts/Create
        public IActionResult Create()
        {
            ViewData["DivisionName"] = new SelectList(_context.AdminBoundaryDivisions, "DivisionGeoCode", "DivisionName");
            return View();
        }

        // POST: AdminBoundaryDistricts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DistrictGeoCode,DistrictName,DistrictNameBangla,DivisionGeoCode,SortingOrder")] AdminBoundaryDistrict adminBoundaryDistrict)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminBoundaryDistrict);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionGeoCode"] = new SelectList(_context.AdminBoundaryDivisions, "DivisionGeoCode", "DivisionGeoCode", adminBoundaryDistrict.DivisionGeoCode);
            return View(adminBoundaryDistrict);
        }

        // GET: AdminBoundaryDistricts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDistrict = await _context.AdminBoundaryDistricts.FindAsync(id);
            if (adminBoundaryDistrict == null)
            {
                return NotFound();
            }
            ViewData["DivisionName"] = new SelectList(_context.AdminBoundaryDivisions, "DivisionGeoCode", "DivisionName", adminBoundaryDistrict.DivisionGeoCode);
            return View(adminBoundaryDistrict);
        }

        // POST: AdminBoundaryDistricts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DistrictGeoCode,DistrictName,DistrictNameBangla,DivisionGeoCode,SortingOrder")] AdminBoundaryDistrict adminBoundaryDistrict)
        {
            if (id != adminBoundaryDistrict.DistrictGeoCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminBoundaryDistrict);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminBoundaryDistrictExists(adminBoundaryDistrict.DistrictGeoCode))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["DivisionName"] = new SelectList(_context.AdminBoundaryDivisions, "DivisionGeoCode", "DivisionName", adminBoundaryDistrict.DivisionGeoCode);
            return View(adminBoundaryDistrict);
        }

        // GET: AdminBoundaryDistricts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDistrict = await _context.AdminBoundaryDistricts
                .Include(a => a.Division)
                .FirstOrDefaultAsync(m => m.DistrictGeoCode == id);
            if (adminBoundaryDistrict == null)
            {
                return NotFound();
            }

            return View(adminBoundaryDistrict);
        }

        // POST: AdminBoundaryDistricts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminBoundaryDistrict = await _context.AdminBoundaryDistricts.FindAsync(id);
            _context.AdminBoundaryDistricts.Remove(adminBoundaryDistrict);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminBoundaryDistrictExists(string id)
        {
            return _context.AdminBoundaryDistricts.Any(e => e.DistrictGeoCode == id);
        }
    }
}
