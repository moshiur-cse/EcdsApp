using EcdsApp.Data;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Theme
{
    //[Authorize]
    public class MetaDataDetailController : Controller
    {
        private readonly DataContext _context;

        public MetaDataDetailController(DataContext context)
        {
            _context = context;
        }

        // GET: MetaDataDetail
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.MetaDataDetails.Include(m => m.ThemeLayerDetails).Include(m => m.TableColumnInfo);
            return View(await dataContext.ToListAsync());
        }

        public async Task<IActionResult> MetaData(int layerId, string topLayerColumnName, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.LayerId = layerId;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;

            if (topLayerColumnName != null)
            {
                var tableId = _context.ThemeLayerDetails.Where(i => i.LayerId == layerId).Select(i => i.TableInfoId).FirstOrDefault();
                var columnId = _context.TableColumnInfos.Where(i => tableId == tableId && i.DbColumnName == topLayerColumnName).Select(i => i.Id).FirstOrDefault();
                return View(await _context.MetaDataDetails.Include(a => a.ThemeLayerDetails.SubThemes.Themes).Where(i => i.LayerId == layerId && i.ColumnId == columnId).FirstOrDefaultAsync());
            }

            return View(await _context.MetaDataDetails.Include(a => a.ThemeLayerDetails.SubThemes.Themes).Where(i => i.LayerId == layerId).FirstOrDefaultAsync());
        }

        // GET: MetaDataDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var metaDataDetail = await _context.MetaDataDetails
                .Include(m => m.ThemeLayerDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaDataDetail == null)
            {
                return NotFound();
            }

            return View(metaDataDetail);
        }

        // GET: MetaDataDetail/Create
        [UserAuthorization]
        public IActionResult Create()
        {
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");

            return View();
        }

        // POST: MetaDataDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("Id,LayerId,ColumnId,Title,Abstract,General,Quality,Completeness,HistoryOfTheDataSet,PurposeOfProduction,ProcessDescription," +
               "TypeOfDataSet,DataSetLanguage,AdditionalInfoSourceForDataSet")] MetaDataDetail metaDataDetail)
        {
            if (ModelState.IsValid)
            {
                var newMetaDataDetailId = (_context.MetaDataDetails.Max(s => (int?)s.Id) ?? 0) + 1;
                metaDataDetail.Id = newMetaDataDetailId;

                _context.Add(metaDataDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");

            return View(metaDataDetail);
        }

        // GET: MetaDataDetail/Edit/5
        [UserAuthorization]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaDataDetail = await _context.MetaDataDetails
                .Include(m => m.ThemeLayerDetails)
                .Include(m => m.ThemeLayerDetails.SubThemes)
                .Include(m => m.ThemeLayerDetails.SubThemes.Themes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaDataDetail == null)
            {
                return NotFound();
            }

            var themeId = metaDataDetail.ThemeLayerDetails.SubThemes.Themes.ThemeId;
            var subThemeId = metaDataDetail.ThemeLayerDetails.SubThemes.SubThemeId;
            var columnId = metaDataDetail.ColumnId;

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", themeId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes.Where(s => s.ThemeId == themeId), "SubThemeId", "SubThemeName", subThemeId);
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails.Where(tl => tl.SubThemeId == subThemeId), "LayerId", "LayerDisplayName", metaDataDetail.LayerId);
            ViewData["ColumnId"] = new SelectList(_context.TableColumnInfos.Where(tl => tl.Id == columnId), "Id", "DisplayName", metaDataDetail.ColumnId);
            return View(metaDataDetail);
        }

        // POST: MetaDataDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LayerId,ColumnId,Title,Abstract,General,Quality,Completeness,HistoryOfTheDataSet,PurposeOfProduction,ProcessDescription," +
                     "TypeOfDataSet,DataSetLanguage,AdditionalInfoSourceForDataSet")] MetaDataDetail metaDataDetail)
        {
            if (id != metaDataDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metaDataDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetaDataDetailExists(metaDataDetail.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var themeId = metaDataDetail.ThemeLayerDetails.SubThemes.Themes.ThemeId;
            var subThemeId = metaDataDetail.ThemeLayerDetails.SubThemes.SubThemeId;
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", themeId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes.Where(s => s.ThemeId == themeId), "SubThemeId", "SubThemeName", subThemeId);
            ViewData["LayerId"] = new SelectList(_context.ThemeLayerDetails.Where(tl => tl.SubThemeId == subThemeId), "LayerId", "LayerDisplayName", metaDataDetail.LayerId);
            ViewData["ColumnId"] = new SelectList(_context.TableColumnInfos.Where(tl => tl.Id == metaDataDetail.ColumnId), "Id", "DisplayName", metaDataDetail.ColumnId);
            return View(metaDataDetail);

        }

        // GET: MetaDataDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaDataDetail = await _context.MetaDataDetails
                .Include(m => m.ThemeLayerDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaDataDetail == null)
            {
                return NotFound();
            }

            return View(metaDataDetail);
        }

        // POST: MetaDataDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metaDataDetail = await _context.MetaDataDetails.FindAsync(id);
            _context.MetaDataDetails.Remove(metaDataDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetaDataDetailExists(int id)
        {
            return _context.MetaDataDetails.Any(e => e.Id == id);
        }
    }
}
