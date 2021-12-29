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
    public class AdminBoundaryUpazilasController : Controller
    {
        private readonly DataContext _context;

        public AdminBoundaryUpazilasController(DataContext context)
        {
            _context = context;
        }

        // GET: AdminBoundaryUpazilas
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.AdminBoundaryUpazilas.Include(a => a.District);
            return View(await dataContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["DistrictName"] = new SelectList(_context.AdminBoundaryDistricts, "DistrictGeoCode", "DistrictName");
            return View();
        }

        // POST: AdminBoundaryUpazilas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UpazilaGeoCode,UpazilaName,UpazilaNameBangla,DistrictGeoCode,SortingOrder")] AdminBoundaryUpazila adminBoundaryUpazila)
        {
            if (id != adminBoundaryUpazila.UpazilaGeoCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminBoundaryUpazila);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminBoundaryUpazilaExists(adminBoundaryUpazila.UpazilaGeoCode))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictName"] = new SelectList(_context.AdminBoundaryDistricts, "DistrictGeoCode", "DistrictName", adminBoundaryUpazila.DistrictGeoCode);
            return View(adminBoundaryUpazila);
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
