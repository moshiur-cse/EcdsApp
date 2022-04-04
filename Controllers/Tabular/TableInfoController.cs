using EcdsApp.Data;
using EcdsApp.Models.TabularModels;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Tabular
{
    [Authorize]
    public class TableInfoController : Controller
    {
        private readonly DataContext _context;

        public TableInfoController(DataContext context)
        {
            _context = context;
        }

        // GET: TableInfo
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.TableInfos
                .Include(t => t.BoundaryInfo)
                .Include(t => t.SubThemes)
                .Include(t => t.SubThemes.Themes);

            ViewData["ColumnTypeId"] = new SelectList(_context.ColumnTypes, "Id", "TypeName");

            return View(await dataContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetTableColumnList(int tableId)
        {
            List<TableColumnInfo> tblColumnInfoList;

            try
            {
                tblColumnInfoList = _context.TableColumnInfos
                    .Include(c => c.TableInfo)
                    .Include(c => c.ColumnType)
                    .Where(c => c.TableId == tableId).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
                tblColumnInfoList = new List<TableColumnInfo>();
            }

            return Json(tblColumnInfoList);
        }

        // GET: TableInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableInfo = await _context.TableInfos
                .Include(t => t.BoundaryInfo)
                .Include(t => t.SubThemes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableInfo == null)
            {
                return NotFound();
            }

            return View(tableInfo);
        }

        // GET: TableInfo/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            ViewData["BoundaryId"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName");
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName");
            return View();
        }

        // POST: TableInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("Id,SubThemeId,BoundaryId,TableName,TableModelName,DisplayName")] TableInfo tableInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tableInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoundaryId"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName", tableInfo.BoundaryId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", tableInfo.SubThemeId);
            return View(tableInfo);
        }

        // GET: TableInfo/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableInfo = await _context.TableInfos.FindAsync(id);
            if (tableInfo == null)
            {
                return NotFound();
            }
            ViewData["BoundaryId"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName", tableInfo.BoundaryId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", tableInfo.SubThemeId);
            return View(tableInfo);
        }

        // POST: TableInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubThemeId,BoundaryId,TableName,TableModelName,DisplayName")] TableInfo tableInfo)
        {
            if (id != tableInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tableInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableInfoExists(tableInfo.Id))
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
            ViewData["BoundaryId"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName", tableInfo.BoundaryId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", tableInfo.SubThemeId);
            return View(tableInfo);
        }

        // GET: TableInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tableInfo = await _context.TableInfos
                .Include(t => t.BoundaryInfo)
                .Include(t => t.SubThemes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tableInfo == null)
            {
                return NotFound();
            }

            return View(tableInfo);
        }

        // POST: TableInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tableInfo = await _context.TableInfos.FindAsync(id);
            _context.TableInfos.Remove(tableInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableInfoExists(int id)
        {
            return _context.TableInfos.Any(e => e.Id == id);
        }
    }
}
