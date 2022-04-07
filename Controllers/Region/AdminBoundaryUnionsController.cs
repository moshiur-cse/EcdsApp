using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models.RegionModels;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EcdsApp.Controllers.Region
{
    public class AdminBoundaryUnionsController : Controller
    {
        private readonly DataContext _context;
        
        public AdminBoundaryUnionsController(DataContext context)
        {
            _context = context;            
        }
        
        // GET: AdminBoundaryUnions

        public async Task<IActionResult> Index()
        {
            var data = await _context.AdminBoundaryUnions
                                .Include(a => a.Upazila)
                                .Take(50)
                                .ToListAsync();            
            int numToShow = 50;
            var totalDataCount = _context.AdminBoundaryUnions.Count();
            ViewData["dataCount"] = totalDataCount;
            if (totalDataCount % numToShow != 0)
            {
                totalDataCount = totalDataCount + (numToShow - (totalDataCount % numToShow));
            }
            ViewData["endPage"] = (totalDataCount / numToShow);
            return View(data);
        }

        public async Task<IActionResult> GetUnionList(int startpage=1, int numToShow=50)
        {
            var totalDataCount = _context.AdminBoundaryUnions.Count();
            if (totalDataCount % numToShow != 0) {
                totalDataCount = totalDataCount + (numToShow - (totalDataCount % numToShow));
            }
            int endPage = (totalDataCount / numToShow);
            if(startpage <= endPage)
            {
                var data = await _context.AdminBoundaryUnions
                                .Include(a => a.Upazila)
                                .Skip((startpage-1) * numToShow)
                                .Take(numToShow)
                                .ToListAsync();
                return Json(data);
            }
            else
            {
                return RedirectToAction("GetUnionList", new { startpage = 1 });
            }            
            
        }

        //public async Task<IActionResult> GetUnionList2()
        //{
            
        //        var data = await _context.AdminBoundaryUnions
        //                        .Include(a => a.Upazila)
        //                        .ToListAsync();
        //        return Json(data);           

        //}

        // GET: AdminBoundaryUnions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryUnion = await _context.AdminBoundaryUnions
                .Include(a => a.Upazila)
                .FirstOrDefaultAsync(m => m.UnionGeoCode == id);
            if (adminBoundaryUnion == null)
            {
                return NotFound();
            }

            return View(adminBoundaryUnion);
        }

        // GET: AdminBoundaryUnions/Create
        public IActionResult Create()
        {
            ViewData["UpazilaGeoCode"] = new SelectList(_context.AdminBoundaryUpazilas.ToList(), "UpazilaGeoCode", "UpazilaName");
            return View();
        }

        // POST: AdminBoundaryUnions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnionGeoCode,OldGeoCode,UnionName,UnionNameBangla,UpazilaGeoCode,MunicipalityGeoCode,MunicipalityName,SortingOrder")] AdminBoundaryUnion adminBoundaryUnion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminBoundaryUnion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UpazilaGeoCode"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaGeoCode", adminBoundaryUnion.UpazilaGeoCode);
            return View(adminBoundaryUnion);
        }

        // GET: AdminBoundaryUnions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryUnion = await _context.AdminBoundaryUnions.FindAsync(id);
            if (adminBoundaryUnion == null)
            {
                return NotFound();
            }
            ViewData["UpazilaGeoCode"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaGeoCode", adminBoundaryUnion.UpazilaGeoCode);
            return View(adminBoundaryUnion);
        }

        // POST: AdminBoundaryUnions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UnionGeoCode,OldGeoCode,UnionName,UnionNameBangla,UpazilaGeoCode,MunicipalityGeoCode,MunicipalityName,SortingOrder")] AdminBoundaryUnion adminBoundaryUnion)
        {
            if (id != adminBoundaryUnion.UnionGeoCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminBoundaryUnion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminBoundaryUnionExists(adminBoundaryUnion.UnionGeoCode))
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
            ViewData["UpazilaGeoCode"] = new SelectList(_context.AdminBoundaryUpazilas, "UpazilaGeoCode", "UpazilaGeoCode", adminBoundaryUnion.UpazilaGeoCode);
            return View(adminBoundaryUnion);
        }

        // GET: AdminBoundaryUnions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminBoundaryUnion = await _context.AdminBoundaryUnions
                .Include(a => a.Upazila)
                .FirstOrDefaultAsync(m => m.UnionGeoCode == id);
            if (adminBoundaryUnion == null)
            {
                return NotFound();
            }

            return View(adminBoundaryUnion);
        }

        // POST: AdminBoundaryUnions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var adminBoundaryUnion = await _context.AdminBoundaryUnions.FindAsync(id);
            _context.AdminBoundaryUnions.Remove(adminBoundaryUnion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminBoundaryUnionExists(string id)
        {
            return _context.AdminBoundaryUnions.Any(e => e.UnionGeoCode == id);
        }
    }
}
