using EcdsApp.Data;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(t => t.SubThemes.Themes)
                .Include(t => t.ThemeLayerTypes)
                .Include(t => t.LegendColorOption)
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
            ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");

            return View();
        }

        // POST: ThemeLayerDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Create([Bind("LayerId,SubThemeId,LayerDisplayName,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FirstAttributeDisplayName," +
            "FirstAttributeName,FirstAttributeCode,SecondAttributeDisplayName,SecondAttributeName,SecondAttributeCode,ThirdAttributeDisplayName,ThirdAttributeName,ThirdAttributeCode,FileLatName,FileLongName," +
            "LegendColorOptionId,LegendColorFieldName,LineColorCode,FillColorCode,Opacity,FillOpacity,LineWeight,BoundaryInfoId,TableInfoId")] 
            ThemeLayerDetail themeLayerDetail, List<IFormFile> geoJsonFile, List<IFormFile> shapeFile)
        {
            if (ModelState.IsValid)
            {
                var newThemeLayerDetId = (_context.ThemeLayerDetails.Max(s => (int?)s.LayerId) ?? 0) + 1;
                themeLayerDetail.LayerId = newThemeLayerDetId;

                if (themeLayerDetail.LayerTypeId != AppStaticBase.LayerTypeTabular)
                {
                    var shapeFileExtList = shapeFile.Select(item => ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Value).Select(Path.GetExtension).ToList();
                    var jsonFileName = ContentDispositionHeaderValue.Parse(geoJsonFile[0].ContentDisposition).FileName.Value;

                    if (!(shapeFileExtList.Contains(".dbf") && shapeFileExtList.Contains(".prj") && shapeFileExtList.Contains(".shp") && shapeFileExtList.Contains(".shx") && Path.GetExtension(jsonFileName).Contains(".json")))
                    {
                        ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                        ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                        ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
                        ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName");
                        ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");
                        return View(themeLayerDetail);
                    }

                    var subThemeObj = _context.SubThemes
                        .Include(s => s.Themes)
                        .FirstOrDefault(s => s.SubThemeId == themeLayerDetail.SubThemeId);
                    var themePath = subThemeObj?.Themes.ThemePath;
                    var subThemePath = subThemeObj?.SubThemePath;

                    var jsonFileFinalName = themeLayerDetail.LayerName + Path.GetExtension(jsonFileName);
                    var jsonFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}\\{jsonFileFinalName}";
                    Directory.CreateDirectory(Directory.GetParent(jsonFilePath).FullName);
                    await using var output = System.IO.File.Create(jsonFilePath);
                    await geoJsonFile[0].CopyToAsync(output);

                    foreach (var file in shapeFile)
                    {
                        var shapeFileName = themeLayerDetail.LayerName + Path.GetExtension(file.FileName);
                        var shapeFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}\\{shapeFileName}";

                        Directory.CreateDirectory(Directory.GetParent(shapeFilePath).FullName);
                        await using var shapeOutput = System.IO.File.Create(shapeFilePath);
                        await file.CopyToAsync(shapeOutput);
                    }

                    themeLayerDetail.LayerFileName = jsonFileFinalName;
                }

                _context.Add(themeLayerDetail);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
            ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName", themeLayerDetail.BoundaryInfoId);
            ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");

            return View(themeLayerDetail);
        }

        public JsonResult GetSubThemeData(int themeId)
        {
            var subThemeList = _context.SubThemes.Where(e => e.ThemeId == themeId).ToList();
            subThemeList.Insert(0, new SubTheme { SubThemeId = 0, SubThemeName = "Select" });

            return Json(new SelectList(subThemeList, "SubThemeId", "SubThemeName"));
        }

        public JsonResult GetLayerData(int subThemeId)
        {
            var layerList = _context.ThemeLayerDetails.Where(e => e.SubThemeId == subThemeId).ToList();
            //layerList.Insert(0, new ThemeLayerDetail { LayerId = 0, LayerName = "Select" });

            return Json(new SelectList(layerList, "LayerId", "LayerDisplayName"));
        }

        public JsonResult GetTableInfoData(int subThemeId, int boundaryId)
        {
            var tableList = _context.TableInfos.Where(e => e.SubThemeId == subThemeId && e.BoundaryId == boundaryId).ToList();
            //tableList.Insert(0, new TableInfo { Id = 0, DisplayName = "Select" });

            return Json(new SelectList(tableList, "Id", "DisplayName"));
        }

        // GET: ThemeLayerDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var themeLayerDetail = await _context.ThemeLayerDetails
                .Include(st => st.SubThemes)
                .Include(st => st.SubThemes.Themes)
                .FirstOrDefaultAsync(st => st.LayerId == id);
            if (themeLayerDetail?.SubThemes?.Themes == null)
                return NotFound();

            var themePath = themeLayerDetail.SubThemes.Themes.ThemePath;
            var subThemePath = themeLayerDetail.SubThemes.SubThemePath;

            var shapeFileList = new List<string>();
            var jsonFileName = "";
            var folderPathDirectory = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}";
            if (Directory.Exists(folderPathDirectory))
            {
                var allFiles = Directory.EnumerateFiles(folderPathDirectory, "*.*", SearchOption.AllDirectories);
                foreach (var file in allFiles)
                {
                    var fileInfo = new FileInfo(file);
                    switch (fileInfo.Extension)
                    {
                        case ".dbf":
                        case ".prj":
                        case ".shp":
                        case ".shx":
                            shapeFileList.Add(fileInfo.Name);
                            break;
                        case ".json":
                            jsonFileName = fileInfo.Name;
                            break;
                    }
                }
            }
            
            ViewBag.ShapeFileList = shapeFileList;
            ViewBag.JsonFileName = jsonFileName;
            ViewBag.LayerType = themeLayerDetail.LayerTypeId;

            var themeId = themeLayerDetail.SubThemes.Themes.ThemeId;
            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName", themeId);
            ViewData["SubThemeId"] = new SelectList(_context.SubThemes.Where(s => s.ThemeId == themeId), "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
            ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName", themeLayerDetail.LegendColorOptionId);
            if (themeLayerDetail.LayerTypeId == AppStaticBase.LayerTypeTabular)
            {
                ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName", themeLayerDetail.BoundaryInfoId);
                ViewData["TableList"] = new SelectList(_context.TableInfos.Where(e => e.SubThemeId == themeLayerDetail.SubThemeId), "Id", "DisplayName", themeLayerDetail.TableInfoId);
            }

            return View(themeLayerDetail);
        }

        // POST: ThemeLayerDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LayerId,SubThemeId,LayerDisplayName,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FirstAttributeDisplayName," +
            "FirstAttributeName,FirstAttributeCode,SecondAttributeDisplayName,SecondAttributeName,SecondAttributeCode,ThirdAttributeDisplayName,ThirdAttributeName,ThirdAttributeCode,FileLatName,FileLongName," +
            "LegendColorOptionId,LegendColorFieldName,LineColorCode,FillColorCode,Opacity,FillOpacity,LineWeight,BoundaryInfoId,TableInfoId")]
            ThemeLayerDetail themeLayerDetail, List<IFormFile> geoJsonFile, List<IFormFile> shapeFile)
        {
            if (id != themeLayerDetail.LayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var subThemeObj = _context.SubThemes
                    .Include(s => s.Themes)
                    .FirstOrDefault(s => s.SubThemeId == themeLayerDetail.SubThemeId);
                if (geoJsonFile.Count > 0 || shapeFile.Count > 0)
                {
                    var themePath = subThemeObj?.Themes.ThemePath;
                    var subThemePath = subThemeObj?.SubThemePath;
                    var folderPathDirectory = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}";
                    if (shapeFile.Count > 0)
                    {
                        var shapeFileExtList = shapeFile.Select(item => ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Value).Select(Path.GetExtension).ToList();
                        if (!(shapeFileExtList.Contains(".dbf") && shapeFileExtList.Contains(".prj") && shapeFileExtList.Contains(".shp") && shapeFileExtList.Contains(".shx")))
                        {
                            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
                            ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");

                            ModelState.AddModelError(string.Empty, "Error! Not All Shape files Uploaded Correctly!");
                            return View(themeLayerDetail);
                        }

                        foreach (var file in Directory.EnumerateFiles(folderPathDirectory, "*", SearchOption.AllDirectories))
                        {
                            var fileInfo = new FileInfo(file);
                            switch (fileInfo.Extension)
                            {
                                case ".dbf":
                                case ".prj":
                                case ".shp":
                                case ".shx":
                                    fileInfo.Delete();
                                    break;
                            }
                        }

                        foreach (var file in shapeFile)
                        {
                            var shapeFileName = themeLayerDetail.LayerName + Path.GetExtension(file.FileName);
                            var shapeFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}\\{shapeFileName}";

                            Directory.CreateDirectory(Directory.GetParent(shapeFilePath).FullName);
                            await using var shapeOutput = System.IO.File.Create(shapeFilePath);
                            await file.CopyToAsync(shapeOutput);
                        }
                    }
                    if (geoJsonFile.Count > 0)
                    {
                        foreach (var file in Directory.EnumerateFiles(folderPathDirectory, "*", SearchOption.AllDirectories))
                        {
                            var fileInfo = new FileInfo(file);
                            switch (fileInfo.Extension)
                            {
                                case ".json":
                                    fileInfo.Delete();
                                    break;
                            }
                        }

                        var jsonFileName = ContentDispositionHeaderValue.Parse(geoJsonFile[0].ContentDisposition).FileName.Value;
                        if (!jsonFileName.Contains(".json"))
                        {
                            ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                            ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                            ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
                            ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");
                            return View(themeLayerDetail);
                        }

                        var jsonFileFinalName = themeLayerDetail.LayerName + Path.GetExtension(jsonFileName);
                        var jsonFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}\\{jsonFileFinalName}";
                        Directory.CreateDirectory(Directory.GetParent(jsonFilePath).FullName);
                        await using var output = System.IO.File.Create(jsonFilePath);
                        await geoJsonFile[0].CopyToAsync(output);

                        themeLayerDetail.LayerFileName = jsonFileFinalName;
                    }
                }
                //in case of theme & sub-theme change
                else
                {
                    var themeLayerExtObj = await _context.ThemeLayerDetails
                        .Include(t => t.SubThemes)
                        .Include(t => t.SubThemes.Themes)
                        .FirstOrDefaultAsync(t => t.LayerId == id);
                    var sourceThemePath = themeLayerExtObj.SubThemes.Themes.ThemePath;
                    var sourceSubThemePath = themeLayerExtObj.SubThemes.SubThemePath;
                    if (themeLayerExtObj.SubThemeId != themeLayerDetail.SubThemeId)
                    {
                        var sourcePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{sourceThemePath?.Trim()}\\{sourceSubThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}";
                    }
                }
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
            ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName", themeLayerDetail.LegendColorOptionId);
            if (themeLayerDetail.LayerTypeId == AppStaticBase.LayerTypeTabular)
            {
                ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName", themeLayerDetail.BoundaryInfoId);
                ViewData["TableList"] = new SelectList(_context.TableInfos.Where(e => e.SubThemeId == themeLayerDetail.SubThemeId), "Id", "DisplayName", themeLayerDetail.TableInfoId);
            }

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
