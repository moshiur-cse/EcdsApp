using EcdsApp.Data;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Theme
{
    [Authorize]
    public class SubThemesController : Controller
    {
        private readonly DataContext _context;

        public SubThemesController(DataContext context)
        {
            _context = context;
        }

        // GET: SubThemes
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.SubThemes.Include(s => s.Themes);

            return View(await dataContext.ToListAsync());
        }

        // GET: SubThemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subTheme = await _context.SubThemes
                .Include(s => s.Themes)
                .FirstOrDefaultAsync(m => m.SubThemeId == id);
            if (subTheme == null)
            {
                return NotFound();
            }

            return View(subTheme);
        }

        // GET: SubThemes/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            return View();
        }

        // POST: SubThemes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("SubThemeId,ThemeId,SubThemeName,SubThemePath")] SubTheme subTheme)
        {
            if (ModelState.IsValid)
            {
                var newSubThemeId = (_context.SubThemes.Max(s => (int?)s.SubThemeId) ?? 0) + 1;
                subTheme.SubThemeId = newSubThemeId;

                _context.Add(subTheme);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", subTheme.ThemeId);
            return View(subTheme);
        }

        // GET: SubThemes/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subTheme = await _context.SubThemes.FindAsync(id);
            if (subTheme == null)
            {
                return NotFound();
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", subTheme.ThemeId);
            return View(subTheme);
        }

        // POST: SubThemes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(int id, [Bind("SubThemeId,ThemeId,SubThemeName,SubThemePath")] SubTheme subTheme)
        {
            if (id != subTheme.SubThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subTheme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubThemeExists(subTheme.SubThemeId))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", subTheme.ThemeId);
            return View(subTheme);
        }

        // GET: SubThemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subTheme = await _context.SubThemes
                .Include(s => s.Themes)
                .FirstOrDefaultAsync(m => m.SubThemeId == id);
            if (subTheme == null)
            {
                return NotFound();
            }

            return View(subTheme);
        }

        // POST: SubThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subTheme = await _context.SubThemes.FindAsync(id);
            _context.SubThemes.Remove(subTheme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubThemeExists(int id)
        {
            return _context.SubThemes.Any(e => e.SubThemeId == id);
        }
    }
}
