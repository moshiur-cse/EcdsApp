using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models.UpazilaWiseInfoModels;

namespace EcdsApp.Controllers.UpazilaWiseInfoControllers
{
    public class UpazilaWiseExposureDataController : Controller
    {
        private readonly DataContext _context;

        public UpazilaWiseExposureDataController(DataContext context)
        {
            _context = context;
        }

        // GET: UpazilaWiseExposureData
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.UpazilaWiseExposureData
                .Include(u => u.DroughtExposure)
                .Include(u => u.EarthquakeExposure)
                .Include(u => u.FloodExposure)
                .Include(u => u.LandSlideExposure)
                .Include(u => u.StormSurgeExposure)
                .Include(u => u.TsunamiExposure)
                .Include(u => u.Upazila);

            return View(await dataContext.ToListAsync());
        }

        // GET: UpazilaWiseExposureData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upazilaWiseExposureData = await _context.UpazilaWiseExposureData
                .Include(u => u.DroughtExposure)
                .Include(u => u.EarthquakeExposure)
                .Include(u => u.FloodExposure)
                .Include(u => u.LandSlideExposure)
                .Include(u => u.StormSurgeExposure)
                .Include(u => u.TsunamiExposure)
                .Include(u => u.Upazila)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upazilaWiseExposureData == null)
            {
                return NotFound();
            }

            return View(upazilaWiseExposureData);
        }

        // GET: UpazilaWiseExposureData/Create
        public IActionResult Create()
        {
            ViewData["DroughtValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName");
            ViewData["EarthquakeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName");
            ViewData["FloodValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName");
            ViewData["LandSlideValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName");
            ViewData["StormSurgeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName");
            ViewData["TsunamiValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName");
            ViewData["UpazilaName"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaName");

            return View();
        }

        // POST: UpazilaWiseExposureData/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UpazilaGeoCode,FloodValue,StormSurgeValue,LandSlideValue,DroughtValue,EarthquakeValue,TsunamiValue")] UpazilaWiseExposureData upazilaWiseExposureData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upazilaWiseExposureData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DroughtValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.DroughtValue);
            ViewData["EarthquakeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.EarthquakeValue);
            ViewData["FloodValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.FloodValue);
            ViewData["LandSlideValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.LandSlideValue);
            ViewData["StormSurgeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.StormSurgeValue);
            ViewData["TsunamiValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.TsunamiValue);
            ViewData["UpazilaGeoCode"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaGeoCode", upazilaWiseExposureData.UpazilaGeoCode);
            return View(upazilaWiseExposureData);
        }

        // GET: UpazilaWiseExposureData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upazilaWiseExposureData = await _context.UpazilaWiseExposureData.FindAsync(id);
            if (upazilaWiseExposureData == null)
            {
                return NotFound();
            }
            ViewData["DroughtValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.DroughtValue);
            ViewData["EarthquakeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.EarthquakeValue);
            ViewData["FloodValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.FloodValue);
            ViewData["LandSlideValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.LandSlideValue);
            ViewData["StormSurgeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.StormSurgeValue);
            ViewData["TsunamiValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.TsunamiValue);
            ViewData["UpazilaName"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaName", upazilaWiseExposureData.UpazilaGeoCode);
            return View(upazilaWiseExposureData);
        }

        // POST: UpazilaWiseExposureData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UpazilaGeoCode,FloodValue,StormSurgeValue,LandSlideValue,DroughtValue,EarthquakeValue,TsunamiValue")] UpazilaWiseExposureData upazilaWiseExposureData)
        {
            if (id != upazilaWiseExposureData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upazilaWiseExposureData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpazilaWiseExposureDataExists(upazilaWiseExposureData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DroughtValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.DroughtValue);
            ViewData["EarthquakeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.EarthquakeValue);
            ViewData["FloodValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.FloodValue);
            ViewData["LandSlideValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.LandSlideValue);
            ViewData["StormSurgeValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.StormSurgeValue);
            ViewData["TsunamiValue"] = new SelectList(_context.ExposureCategories, "Id", "CategoryName", upazilaWiseExposureData.TsunamiValue);
            ViewData["UpazilaGeoCode"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaGeoCode", upazilaWiseExposureData.UpazilaGeoCode);
            return View(upazilaWiseExposureData);
        }

        // GET: UpazilaWiseExposureData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upazilaWiseExposureData = await _context.UpazilaWiseExposureData
                .Include(u => u.DroughtExposure)
                .Include(u => u.EarthquakeExposure)
                .Include(u => u.FloodExposure)
                .Include(u => u.LandSlideExposure)
                .Include(u => u.StormSurgeExposure)
                .Include(u => u.TsunamiExposure)
                .Include(u => u.Upazila)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upazilaWiseExposureData == null)
            {
                return NotFound();
            }

            return View(upazilaWiseExposureData);
        }

        // POST: UpazilaWiseExposureData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upazilaWiseExposureData = await _context.UpazilaWiseExposureData.FindAsync(id);
            _context.UpazilaWiseExposureData.Remove(upazilaWiseExposureData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpazilaWiseExposureDataExists(int id)
        {
            return _context.UpazilaWiseExposureData.Any(e => e.Id == id);
        }
    }
}
