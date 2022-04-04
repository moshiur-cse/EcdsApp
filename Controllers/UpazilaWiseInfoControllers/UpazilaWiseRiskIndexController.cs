using EcdsApp.Data;
using EcdsApp.Models.UpazilaWiseInfoModels;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.UpazilaWiseInfoControllers
{
    [Authorize]
    public class UpazilaWiseRiskIndexController : Controller
    {
        private readonly DataContext _context;

        public UpazilaWiseRiskIndexController(DataContext context)
        {
            _context = context;
        }

        // GET: UpazilaWiseRiskIndex
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.UpazilaWiseRiskIndex
                .Include(u => u.Upazila);

            return View(await dataContext.ToListAsync());
        }

        // GET: UpazilaWiseRiskIndex/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upazilaWiseRiskIndex = await _context.UpazilaWiseRiskIndex
                .Include(u => u.Upazila)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upazilaWiseRiskIndex == null)
            {
                return NotFound();
            }

            return View(upazilaWiseRiskIndex);
        }

        // GET: UpazilaWiseRiskIndex/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            ViewData["UpazilaName"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaName");
            return View();
        }

        // POST: UpazilaWiseRiskIndex/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("Id,UpazilaGeoCode,PeopleAffectedNaturalDisaster,HeatStressVulnerability,GroundWaterVulnerability,MangroveForestVulnerability,LivestockLandVulnerability," +
           "WaterAvailabilityVulnerability,CropYieldVulnerability,LivestockHealthVulnerability,AgriLandAvailabilityVulnerability,FishCultureVulnerability,FishCaptureVulnerability,RailNetworkVulnerability," +
           "RoadNetworkVulnerability")] UpazilaWiseRiskIndex upazilaWiseRiskIndex)
        {
            if (ModelState.IsValid)
            {
                var newId = (_context.UpazilaWiseRiskIndex.Max(s => (int?)s.Id) ?? 0) + 1;
                upazilaWiseRiskIndex.Id = newId;

                _context.Add(upazilaWiseRiskIndex);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["UpazilaName"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaName", upazilaWiseRiskIndex.UpazilaGeoCode);
            return View(upazilaWiseRiskIndex);
        }

        // GET: UpazilaWiseRiskIndex/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upazilaWiseRiskIndex = await _context.UpazilaWiseRiskIndex.FindAsync(id);
            if (upazilaWiseRiskIndex == null)
            {
                return NotFound();
            }

            ViewData["UpazilaName"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaName", upazilaWiseRiskIndex.UpazilaGeoCode);
            return View(upazilaWiseRiskIndex);
        }

        // POST: UpazilaWiseRiskIndex/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UpazilaGeoCode,PeopleAffectedNaturalDisaster,HeatStressVulnerability,GroundWaterVulnerability,MangroveForestVulnerability,LivestockLandVulnerability,WaterAvailabilityVulnerability,CropYieldVulnerability,LivestockHealthVulnerability,AgriLandAvailabilityVulnerability,FishCultureVulnerability,FishCaptureVulnerability,RailNetworkVulnerability,RoadNetworkVulnerability")] UpazilaWiseRiskIndex upazilaWiseRiskIndex)
        {
            if (id != upazilaWiseRiskIndex.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upazilaWiseRiskIndex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpazilaWiseRiskIndexExists(upazilaWiseRiskIndex.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["UpazilaName"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaName", upazilaWiseRiskIndex.UpazilaGeoCode);
            return View(upazilaWiseRiskIndex);
        }

        // GET: UpazilaWiseRiskIndex/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upazilaWiseRiskIndex = await _context.UpazilaWiseRiskIndex
                .Include(u => u.Upazila)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upazilaWiseRiskIndex == null)
            {
                return NotFound();
            }

            return View(upazilaWiseRiskIndex);
        }

        // POST: UpazilaWiseRiskIndex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upazilaWiseRiskIndex = await _context.UpazilaWiseRiskIndex.FindAsync(id);
            _context.UpazilaWiseRiskIndex.Remove(upazilaWiseRiskIndex);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpazilaWiseRiskIndexExists(int id)
        {
            return _context.UpazilaWiseRiskIndex.Any(e => e.Id == id);
        }
    }
}
