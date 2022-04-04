using EcdsApp.Data;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.ThemeLayer
{
    [Authorize]
    public class LayerLegendColorsController : Controller
    {
        private readonly DataContext _context;

        public LayerLegendColorsController(DataContext context)
        {
            _context = context;
        }

        // GET: LayerLegendColors
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.LayerLegendColors.Include(l => l.ThemeLayerDetails);
            return View(await dataContext.ToListAsync());
        }

        // GET: LayerLegendColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var layerLegendColor = await _context.LayerLegendColors
                .Include(l => l.ThemeLayerDetails)
                .FirstOrDefaultAsync(m => m.LayerLegendColorId == id);
            if (layerLegendColor == null)
            {
                return NotFound();
            }

            return View(layerLegendColor);
        }

        // GET: LayerLegendColors/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails.Where(t => t.LegendColorOptionId == 1), "LayerId", "LayerDisplayName");
            return View();
        }

        // POST: LayerLegendColors/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [UserAuthorization]
        public IActionResult Create(LayerLegendColor formData)
        {
            var result = "Error";
            if (!ModelState.IsValid)
                return Json(result);

            var newLayerLegendColorId = (_context.LayerLegendColors.Max(s => (int?)s.LayerLegendColorId) ?? 0) + 1;

            formData.LayerLegendColorId = newLayerLegendColorId;
            _context.Add(formData);
            var response = _context.SaveChanges() > 0;
            result = response ? "Success" : "Error";

            //return RedirectToAction(nameof(Index));

            //ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails, "LayerId", "LayerFileName", layerLegendColor.LayerId);
            return Json(result);
        }

        [HttpPost]
        public IActionResult DeleteLayerLegendColorInfo(int layerLegendColorId)
        {
            var layerLegendColorObj = _context.LayerLegendColors.Find(layerLegendColorId);
            _context.LayerLegendColors.Remove(layerLegendColorObj);

            var response = _context.SaveChanges() > 0;
            var result = response ? "Success" : "Error";

            return Json(result);
        }

        [HttpGet]
        public IActionResult GetLayerLegendColorInfo(int layerId)
        {
            List<LayerLegendColor> legendColorDataList;

            try
            {
                legendColorDataList = _context.LayerLegendColors
                    .Include(c => c.ThemeLayerDetails)
                    .Where(c => c.LayerId == layerId).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
                legendColorDataList = new List<LayerLegendColor>();
            }

            return Json(legendColorDataList);
        }

        [HttpGet]
        public IActionResult GetLegendColorPropertyInfo(int layerLegendColorId)
        {
            var layerLegendColorObj = _context.LayerLegendColors.FirstOrDefault(l => l.LayerLegendColorId == layerLegendColorId);

            return Json(layerLegendColorObj);
        }

        // GET: LayerLegendColors/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var layerLegendColor = await _context.LayerLegendColors.FindAsync(id);
            if (layerLegendColor == null)
            {
                return NotFound();
            }
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails, "LayerId", "LayerFileName", layerLegendColor.LayerId);
            return View(layerLegendColor);
        }

        // POST: LayerLegendColors/Edit/5
        [HttpPost]
        [UserAuthorization]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(LayerLegendColor formData)
        {
            var result = "Error";
            if (!ModelState.IsValid)
                return Json(result);

            try
            {
                _context.Update(formData);
                var response = _context.SaveChanges() > 0;
                result = response ? "Success" : "Error";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LayerLegendColorExists(formData.LayerLegendColorId))
                {
                    return NotFound();
                }
                throw;
            }

            return Json(result);
        }

        // GET: LayerLegendColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var layerLegendColor = await _context.LayerLegendColors
                .Include(l => l.ThemeLayerDetails)
                .FirstOrDefaultAsync(m => m.LayerLegendColorId == id);
            if (layerLegendColor == null)
            {
                return NotFound();
            }

            return View(layerLegendColor);
        }

        // POST: LayerLegendColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var layerLegendColor = await _context.LayerLegendColors.FindAsync(id);
            _context.LayerLegendColors.Remove(layerLegendColor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LayerLegendColorExists(int id)
        {
            return _context.LayerLegendColors.Any(e => e.LayerLegendColorId == id);
        }
    }
}
