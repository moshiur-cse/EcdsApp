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
    public class AdminBoundaryDivisionsController : Controller
    {
        private readonly DataContext _context;

        public AdminBoundaryDivisionsController(DataContext context)
        {
            _context = context;
        }

        // GET: AdminBoundaryDivisions
        public async Task<IActionResult> Index()
        {
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminBoundaryDivisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
