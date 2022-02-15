using EcdsApp.Data;
using EcdsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers
{
    public class BundleDetailController : Controller
    {
        private readonly DataContext _context;

        public BundleDetailController(DataContext context)
        {
            _context = context;
        }

        // GET: BundleDetail
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.BundleDetails
                .Include(b => b.ThemeLayerDetails);

            return View(await dataContext.ToListAsync());
        }

        public async Task<IActionResult> BundleData(int layerId, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.LayerId = layerId;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;
            return View(await _context.BundleDetails.Include(a => a.ThemeLayerDetails.SubThemes.Themes).Where(i => i.LayerId == layerId).ToListAsync());
        }

        // GET: BundleDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bundleDetail = await _context.BundleDetails
                .Include(b => b.ThemeLayerDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bundleDetail == null)
            {
                return NotFound();
            }

            return View(bundleDetail);
        }

        // GET: BundleDetail/Create
        public IActionResult Create()
        {
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails, "LayerId", "LayerDisplayName");

            return View();
        }

        // POST: BundleDetail/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BundleDetail formData)
        {
            var result = "Error";
            if (!ModelState.IsValid)
                return Json(result);
            var bundleDetId = (_context.BundleDetails.Max(s => (int?)s.Id) ?? 0) + 1;
            formData.Id = bundleDetId;

            _context.Add(formData);
            var response = await _context.SaveChangesAsync() > 0;
            result = response ? "Success" : "Error";

            return Json(result);
        }

        [HttpGet]
        public IActionResult GetBundleInfo(int layerId)
        {
            List<BundleDetail> bundleDetDataList;

            try
            {
                bundleDetDataList = _context.BundleDetails
                    .Include(c => c.ThemeLayerDetails)
                    .Where(c => c.LayerId == layerId).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
                bundleDetDataList = new List<BundleDetail>();
            }

            return Json(bundleDetDataList);
        }

        [HttpGet]
        public IActionResult GetBundlePropertyInfo(int bundleId)
        {
            var bundleObj = _context.BundleDetails.FirstOrDefault(l => l.Id == bundleId);

            return Json(bundleObj);
        }

        [HttpPost]
        public IActionResult DeleteBundleInfo(int bundleId)
        {
            var bundleObj = _context.BundleDetails.Find(bundleId);
            _context.BundleDetails.Remove(bundleObj);

            var response = _context.SaveChanges() > 0;
            var result = response ? "Success" : "Error";

            return Json(result);
        }

        // GET: BundleDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bundleDetail = await _context.BundleDetails
                .Include(m => m.ThemeLayerDetails)
                .Include(m => m.ThemeLayerDetails.SubThemes)
                .Include(m => m.ThemeLayerDetails.SubThemes.Themes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bundleDetail == null)
            {
                return NotFound();
            }

            var themeId = bundleDetail.ThemeLayerDetails.SubThemes.Themes.ThemeId;
            var subThemeId = bundleDetail.ThemeLayerDetails.SubThemes.SubThemeId;

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", themeId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes.Where(s => s.ThemeId == themeId), "SubThemeId", "SubThemeName", subThemeId);
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails.Where(tl => tl.SubThemeId == subThemeId), "LayerId", "LayerDisplayName", bundleDetail.LayerId);

            return View(bundleDetail);
        }

        // POST: BundleDetail/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BundleDetail formData)
        {
            var result = "Error";
            if (!ModelState.IsValid)
                return Json(result);

            try
            {
                _context.Update(formData);
                var response = await _context.SaveChangesAsync() > 0;
                result = response ? "Success" : "Error";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BundleDetailExists(formData.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return Json(result);
        }

        // GET: BundleDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bundleDetail = await _context.BundleDetails
                .Include(b => b.ThemeLayerDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bundleDetail == null)
            {
                return NotFound();
            }

            return View(bundleDetail);
        }

        // POST: BundleDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bundleDetail = await _context.BundleDetails.FindAsync(id);
            _context.BundleDetails.Remove(bundleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BundleDetailExists(int id)
        {
            return _context.BundleDetails.Any(e => e.Id == id);
        }
    }
}
