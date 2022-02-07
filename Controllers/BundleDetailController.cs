using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models;

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

            return View();
        }

        // POST: BundleDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LayerId,FieldName,FieldDescription,FieldUnit")] BundleDetail bundleDetail)
        {
            if (ModelState.IsValid)
            {
                var bundleDetId = (_context.BundleDetails.Max(s => (int?)s.Id) ?? 0) + 1;
                bundleDetail.Id = bundleDetId;

                _context.Add(bundleDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");

            return View(bundleDetail);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LayerId,FieldName,FieldDescription,FieldUnit")] BundleDetail bundleDetail)
        {
            if (id != bundleDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bundleDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BundleDetailExists(bundleDetail.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var themeId = bundleDetail.ThemeLayerDetails.SubThemes.Themes.ThemeId;
            var subThemeId = bundleDetail.ThemeLayerDetails.SubThemes.SubThemeId;

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", themeId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes.Where(s => s.ThemeId == themeId), "SubThemeId", "SubThemeName", subThemeId);
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails.Where(tl => tl.SubThemeId == subThemeId), "LayerId", "LayerDisplayName", bundleDetail.LayerId);

            return View(bundleDetail);
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
