using EcdsApp.Data;
using EcdsApp.Models;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Region
{
    [Authorize]
    public class AdminBoundaryDivisionsController : Controller
    {
        private readonly DataContext _context;

        public AdminBoundaryDivisionsController(DataContext context)
        {
            _context = context;
        }

        // GET: AdminBoundaryDivisions
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminBoundaryDivisions.ToListAsync());
        }

        public async Task<IActionResult> SummaryData(string adminCode, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.DivisionCode = adminCode;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryDivisions.Where(i => i.DivisionGeoCode == adminCode).ToListAsync());
        }

        public async Task<IActionResult> DetailsData(int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            return View(await _context.AdminBoundaryDivisions.ToListAsync());
        }

        // GET: AdminBoundaryDivisions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDivision = await _context.AdminBoundaryDivisions
                .FirstOrDefaultAsync(m => m.DivisionGeoCode == id);
            if (adminBoundaryDivision == null)
            {
                return NotFound();
            }

            return View(adminBoundaryDivision);
        }

        // GET: AdminBoundaryDivisions/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminBoundaryDivisions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("DivisionGeoCode,DivisionName,DivisionNameBangla,SortingOrder")] AdminBoundaryDivision adminBoundaryDivision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminBoundaryDivision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminBoundaryDivision);
        }

        // GET: AdminBoundaryDivisions/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDivision = await _context.AdminBoundaryDivisions.FindAsync(id);
            if (adminBoundaryDivision == null)
            {
                return NotFound();
            }
            return View(adminBoundaryDivision);
        }

        // POST: AdminBoundaryDivisions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(string id, [Bind("DivisionGeoCode,DivisionName,DivisionNameBangla,SortingOrder")] AdminBoundaryDivision adminBoundaryDivision)
        {
            if (id != adminBoundaryDivision.DivisionGeoCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminBoundaryDivision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminBoundaryDivisionExists(adminBoundaryDivision.DivisionGeoCode))
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
            return View(adminBoundaryDivision);
        }

        // GET: AdminBoundaryDivisions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryDivision = await _context.AdminBoundaryDivisions
                .FirstOrDefaultAsync(m => m.DivisionGeoCode == id);
            if (adminBoundaryDivision == null)
            {
                return NotFound();
            }

            return View(adminBoundaryDivision);
        }

        // POST: AdminBoundaryDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminBoundaryDivision = await _context.AdminBoundaryDivisions.FindAsync(id);
            _context.AdminBoundaryDivisions.Remove(adminBoundaryDivision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminBoundaryDivisionExists(string id)
        {
            return _context.AdminBoundaryDivisions.Any(e => e.DivisionGeoCode == id);
        }
    }
}
