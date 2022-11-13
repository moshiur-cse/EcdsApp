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
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            return View();
        }

        // POST: TableInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("SubThemeId,BoundaryId,TableName,TableModelName,DisplayName")] TableInfo tableInfo)
        {
            if (ModelState.IsValid)
            {
                var id = _context.TableInfos.Max(x => x.Id) + 1;
                tableInfo.Id = id;
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

        [HttpPost]
        //[UserAuthorization]
        public async Task<IActionResult> AddTableColumnInfo(TableColumnInfo form)
        {
			if (ModelState.IsValid)
			{
                var newDataId = _context.TableColumnInfos.Max(x=>x.Id)+1;
                form.Id = newDataId;
                _context.Add(form);
                await _context.SaveChangesAsync();
                return Json("success");
			}
            return Json("failed");
        }

        [HttpPost]
        //[UserAuthorization]
        public async Task<IActionResult> UpdateTableColumnInfo( TableColumnInfo form)
        {
            if (ModelState.IsValid)
            {
                _context.Update(form);
                await _context.SaveChangesAsync();
                return Json("success");
            }
            return Json("failed");
        }

        [HttpGet]
        //[UserAuthorization]
        public async Task<IActionResult> DeleteTableColumnInfo(int id)
        {
            if (ModelState.IsValid)
            {
                var tabColInfo = await _context.TableColumnInfos.FindAsync(id);
                if(tabColInfo != null)
                {
                    _context.Remove(tabColInfo);
                    await _context.SaveChangesAsync();
                    return Json("success");
                }
                
                
            }
            return Json("failed");
        }

        public async Task<IActionResult> TableColumnTypeList()
		{
            return Json(await _context.ColumnTypes.ToListAsync());
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
