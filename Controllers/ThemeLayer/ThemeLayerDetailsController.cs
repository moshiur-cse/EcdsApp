using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcdsApp.Data;
using EcdsApp.Models.TabularModels;
using EcdsApp.Models.ThemeModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace EcdsApp.Controllers.ThemeLayer
{
    public class ThemeLayerDetailsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ThemeLayerDetailsController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: ThemeLayerDetails
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ThemeLayerDetails
                .Include(t => t.SubThemes)
                .Include(t => t.ThemeLayerTypes)
                .Include(t => t.BoundaryInfo)
                .Include(t => t.TableInfo);

            return View(await dataContext.ToListAsync());
        }

        // GET: ThemeLayerDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeLayerDetail = await _context.ThemeLayerDetails
                .Include(t => t.SubThemes)
                .Include(t => t.ThemeLayerTypes)
                .FirstOrDefaultAsync(m => m.LayerId == id);
            if (themeLayerDetail == null)
            {
                return NotFound();
            }

            return View(themeLayerDetail);
        }

        // GET: ThemeLayerDetails/Create
        public IActionResult Create()
        {
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName");
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName");
            ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName");

            return View();
        }

        // POST: ThemeLayerDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create([Bind("LayerId,SubThemeId,LayerPath,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FirstAttributeDisplayName," +
            "FirstAttributeName,FirstAttributeCode,SecondAttributeDisplayName,SecondAttributeName,SecondAttributeCode,ThirdAttributeDisplayName,ThirdAttributeName,ThirdAttributeCode,FileLatName,FileLongName," +
            "IsLegendColor,LegendColorFieldName,LineColorCode,FillColorCode,Opacity,FillOpacity,LineWeight,BoundaryInfoId,TableInfoId")] 
            ThemeLayerDetail themeLayerDetail, List<IFormFile> geoJsonFile, List<IFormFile> shapeFile)
        {
            if (ModelState.IsValid)
            {
                var shapeFileExtList = shapeFile.Select(item => ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Value).Select(Path.GetExtension).ToList();
                var jsonFileName = ContentDispositionHeaderValue.Parse(geoJsonFile[0].ContentDisposition).FileName.Value;

                if (!(shapeFileExtList.Contains(".dbf") && shapeFileExtList.Contains(".prj") && shapeFileExtList.Contains(".shp") && shapeFileExtList.Contains(".shx") && Path.GetExtension(jsonFileName).Contains(".json")))
                {
                    ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                    ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                    ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);

                    return View(themeLayerDetail);
                }
                
                var subThemeObj = _context.SubThemes
                    .Include(s => s.Themes)
                    .FirstOrDefault(s => s.SubThemeId == themeLayerDetail.SubThemeId);
                var themePath = subThemeObj?.Themes.ThemePath;
                var subThemePath = subThemeObj?.SubThemePath;

                var jsonFileFinalName = themeLayerDetail.LayerPath + Path.GetExtension(jsonFileName);
                var jsonFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerPath.Trim()}\\{jsonFileFinalName}";
                Directory.CreateDirectory(Directory.GetParent(jsonFilePath).FullName);
                await using var output = System.IO.File.Create(jsonFilePath);
                await geoJsonFile[0].CopyToAsync(output);

                foreach (var file in shapeFile)
                {
                    var shapeFileName = themeLayerDetail.LayerPath + Path.GetExtension(file.FileName);
                    var shapeFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerPath.Trim()}\\{shapeFileName}";

                    Directory.CreateDirectory(Directory.GetParent(shapeFilePath).FullName);
                    await using var shapeOutput = System.IO.File.Create(shapeFilePath);
                    await file.CopyToAsync(shapeOutput);
                }

                var newThemeLayerDetId = (_context.ThemeLayerDetails.Max(s => (int?)s.LayerId) ?? 0) + 1;
                themeLayerDetail.LayerId = newThemeLayerDetId;
                themeLayerDetail.LayerFileName = jsonFileFinalName;

                _context.Add(themeLayerDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);

            return View(themeLayerDetail);
        }

        public JsonResult GetSubThemeData(int themeId)
        {
            var subThemeList = _context.SubThemes.Where(e => e.ThemeId == themeId).ToList();
            subThemeList.Insert(0, new SubTheme { SubThemeId = 0, SubThemeName = "Select" });

            return Json(new SelectList(subThemeList, "SubThemeId", "SubThemeName"));
        }

        public JsonResult GetTableInfoData(int subThemeId)
        {
            var tableList = _context.TableInfos.Where(e => e.SubThemeId == subThemeId).ToList();
            tableList.Insert(0, new TableInfo { Id = 0, DisplayName = "Select" });

            return Json(new SelectList(tableList, "Id", "DisplayName"));
        }

        // GET: ThemeLayerDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var themeLayerDetail = await _context.ThemeLayerDetails
                .Include(st => st.SubThemes)
                .FirstOrDefaultAsync(st => st.LayerId == id);
            var themeLayerObj = await _context.Themes.FirstOrDefaultAsync(t => t.ThemeId == themeLayerDetail.SubThemes.ThemeId);
            if (themeLayerDetail == null || themeLayerObj == null)
                return NotFound();

            ViewBag.JsonFileName = themeLayerDetail.LayerFileName;
            ViewBag.ShapeFileName = "Not Defined";

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", themeLayerObj.ThemeId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
            return View(themeLayerDetail);
        }

        // POST: ThemeLayerDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LayerId,SubThemeId,LayerPath,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FirstAttributeDisplayName," +
            "FirstAttributeName,FirstAttributeCode,SecondAttributeDisplayName,SecondAttributeName,SecondAttributeCode,ThirdAttributeDisplayName,ThirdAttributeName,ThirdAttributeCode,FileLatName,FileLongName," +
            "IsLegendColor,LegendColorFieldName,LineColorCode,FillColorCode,Opacity,FillOpacity,LineWeight")] ThemeLayerDetail themeLayerDetail)
        {
            if (id != themeLayerDetail.LayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(themeLayerDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeLayerDetailExists(themeLayerDetail.LayerId))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
            return View(themeLayerDetail);
        }

        // GET: ThemeLayerDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var themeLayerDetail = await _context.ThemeLayerDetails
                .Include(t => t.SubThemes)
                .Include(t => t.ThemeLayerTypes)
                .FirstOrDefaultAsync(m => m.LayerId == id);
            if (themeLayerDetail == null)
            {
                return NotFound();
            }

            return View(themeLayerDetail);
        }

        // POST: ThemeLayerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeLayerDetail = await _context.ThemeLayerDetails.FindAsync(id);
            _context.ThemeLayerDetails.Remove(themeLayerDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeLayerDetailExists(int id)
        {
            return _context.ThemeLayerDetails.Any(e => e.LayerId == id);
        }
    }
}
