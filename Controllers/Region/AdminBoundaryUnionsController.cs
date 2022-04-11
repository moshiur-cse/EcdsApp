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
using EcdsApp.Models.ViewModels;

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

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult JqueryDataTableUnionList()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                //var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumn = Request.Form["order[0][column]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault().Trim();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var unionData = _context.AdminBoundaryUnions
                                .Include(a => a.Upazila).AsQueryable();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    
                    if (sortColumnDirection == "asc")
                    {
                        switch (sortColumn)
                        {
                            case "1":
                                unionData = unionData.OrderBy(x => x.Upazila.UpazilaName);
                                break;
                            case "2":
                                unionData = unionData.OrderBy(x => x.UnionName);
                                break;
                            case "3":
                                unionData = unionData.OrderBy(x => x.UnionNameBangla);
                                break;
                            case "4":
                                unionData = unionData.OrderBy(x => x.MunicipalityName);
                                break;
                            case "5":
                                unionData = unionData.OrderBy(x => x.MunicipalityGeoCode);
                                break;
                            case "6":
                                unionData = unionData.OrderBy(x => x.SortingOrder);
                                break;
                        }
                    }


                    else
                    {
                        switch (sortColumn)
                        {
                            case "1":
                                unionData = unionData.OrderByDescending(x => x.Upazila.UpazilaName);
                                break;
                            case "2":
                                unionData = unionData.OrderByDescending(x => x.UnionName);
                                break;
                            case "3":
                                unionData = unionData.OrderByDescending(x => x.UnionNameBangla);
                                break;
                            case "4":
                                unionData = unionData.OrderByDescending(x => x.MunicipalityName);
                                break;
                            case "5":
                                unionData = unionData.OrderByDescending(x => x.MunicipalityGeoCode);
                                break;
                            case "6":
                                unionData = unionData.OrderByDescending(x => x.SortingOrder);
                                break;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    
                    unionData = unionData.Where(m => m.Upazila.UpazilaName.Contains(searchValue)
                                                || m.UpazilaGeoCode.Contains(searchValue)
                                                || m.SortingOrder.ToString().Contains(searchValue)
                                                || m.MunicipalityGeoCode.Contains(searchValue)
                                                || m.MunicipalityName.Contains(searchValue)
                                                || m.UnionName.Contains(searchValue)
                                                || m.UnionNameBangla.Contains(searchValue));
                }
                recordsTotal = unionData.Count();
                var data = unionData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    


    //public async Task<IActionResult> GetUnionList2(int draw = 1, int numToShow = 50)
    //{
            
    //    var totalDataCount = _context.AdminBoundaryUnions.Count();
    //    if (totalDataCount % numToShow != 0)
    //    {
    //        totalDataCount = totalDataCount + (numToShow - (totalDataCount % numToShow));
    //    }
    //    int endPage = (totalDataCount / numToShow);
    //    if (draw <= endPage)
    //    {
                
    //        var data = await _context.AdminBoundaryUnions
    //                        .Include(a => a.Upazila)
    //                        .Skip((draw - 1) * numToShow)
    //                        .Take(numToShow)
    //                        .ToListAsync();

    //        AjaxUnionViewModel ajaxUnionData = new()
    //        {
    //            Draw= draw,
    //            RecordsTotal= totalDataCount,
    //            RecordsFiltered= totalDataCount,
    //            Data = data                    
    //        };
    //        return Json(ajaxUnionData);
    //    }
    //    else
    //    {
    //        return RedirectToAction("GetUnionList", new { startpage = 1 });
    //    }

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
