using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;

namespace EcdsApp.Controllers.Theme
{
    public class ThemesController : Controller
    {
        private readonly DataContext _context;

        public ThemesController(DataContext context)
        {
            _context = context;
        }

        // GET: Themes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Themes.ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(m => m.ThemeId == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // GET: Themes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Themes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThemeId,ThemeName,ThemePath")] Models.ThemeModels.Theme theme)
        {
            if (!ModelState.IsValid)
                return View(theme);

            var newThemeId = (_context.Themes.Max(s => (int?)s.ThemeId) ?? 0) + 1;
            theme.ThemeId = newThemeId;

            _context.Add(theme);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            return View(theme);
        }

        // POST: Themes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThemeId,ThemeName,ThemePath")] Models.ThemeModels.Theme theme)
        {
            if (id != theme.ThemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(theme.ThemeId))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(m => m.ThemeId == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theme = await _context.Themes.FindAsync(id);
            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeExists(int id)
        {
            return _context.Themes.Any(e => e.ThemeId == id);
        }
    }
}
