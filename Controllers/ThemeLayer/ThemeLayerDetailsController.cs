using ClosedXML.Excel;
using EcdsApp.Data;
using EcdsApp.Models.TabularModels;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.ViewModels;
using EcdsApp.Security;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EcdsApp.Models.HitCountAndLogModels;
using DocumentFormat.OpenXml.Drawing;
using EcdsApp.Models.ViewModels.Map;
using EcdsApp.Models.ThemeModels;

namespace EcdsApp.Controllers.ThemeLayer
{
    [Authorize]
    public class ThemeLayerDetailsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Utility _utility;

        public ThemeLayerDetailsController(DataContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _utility = new Utility(_context);
        }

        // GET: ThemeLayerDetails
        [UserAuthorization]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userPerComponents = _context.RoleWiseComponents
                .Where(r => r.UserRoleId == user.UserRoleId).Select(r => r.SubThemeId).ToList();

            var permToDeleteData = _utility.DoesHavePermissionToDeleteData(user.UserRoleId, "Delete");
            var permToAddData = _utility.DoesHavePermissionToAddData(user.UserRoleId, "Create");
            var permToEditData = _utility.DoesHavePermissionToEditData(user.UserRoleId, "Edit");

            var dataContext = _context.ThemeLayerDetails
                .Include(t => t.SubThemes.Themes)
                .Include(t => t.ThemeLayerTypes)
                .Include(t => t.LegendColorOption)
                .Include(t => t.BoundaryInfo)
                .Include(t => t.TableInfo)
                .Include(t => t.User)
                .Include(t => t.DataVerificationState)
                .Where(tld => userPerComponents.Contains(tld.SubThemeId));


            ViewBag.IsPermittedToAddData = permToAddData;
            ViewBag.IsPermittedToEditData = permToEditData;       
            ViewBag.IsPermittedToDeleteData = permToDeleteData;       
            ViewBag.isSystemAdmin = user.UserRoleId != null && user.UserRoleId == "f3b152e7-5e27-4d94-8101-5994faef8fdd" ? true:false;
            return View(await dataContext.ToListAsync());
        }


        public async Task<IActionResult> ApproveDataByAdmin(int id)
        {
            var layer = await _context.ThemeLayerDetails.FindAsync(id);
            if (layer != null)
            {
                layer.DataVerificationStateId = 1;
                _context.Update(layer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
            
        }

        // GET: ThemeLayerDetails/Details/5
        [UserAuthorization]
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
        [UserAuthorization]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            var userPerComponents = _context.RoleWiseComponents
                .Where(r => r.UserRoleId == user.UserRoleId).Select(r => r.SubThemeId).ToList();
            var ThemeList = _context.SubThemes.Where(e => userPerComponents.Contains(e.SubThemeId)).Select(k => new
            {
                ThemeId = k.ThemeId,
                ThemeName = k.Themes.ThemeName
            }).Distinct().ToList();

            ViewData["ThemeId"] = new SelectList(ThemeList, "ThemeId", "ThemeName");
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
        [UserAuthorization]
        public async Task<IActionResult> Create([Bind("LayerId,SubThemeId,LayerDisplayName,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FileLatName,FileLongName," +
            "LegendColorOptionId,LegendColorFieldName,LineColorCode,FillColorCode,Opacity,FillOpacity,LineWeight,BoundaryInfoId,TableInfoId,GeneratedAt,UserId,ReadStatus")]
            ThemeLayerDetail themeLayerDetail, List<IFormFile> geoJsonFile, List<IFormFile> shapeFile)
        {
            if (ModelState.IsValid)
            {

                //========= Set theme LayerName from layerDisplay Name by removing space with _;
                themeLayerDetail.LayerName=themeLayerDetail.LayerDisplayName.Replace(" ", "_");
                themeLayerDetail.LayerName = themeLayerDetail.LayerName.ToLower();
                
                var newThemeLayerDetId = (_context.ThemeLayerDetails.Max(s => (int?)s.LayerId) ?? 0) + 1;
                themeLayerDetail.LayerId = newThemeLayerDetId;
                              
                //==== Check for the layer name if that is unique
                bool isNotUnique = await _context.ThemeLayerDetails.AnyAsync(x => x.LayerName == themeLayerDetail.LayerName);
                if (isNotUnique)
                {
                    ModelState.AddModelError("LayerName","The Layer name already exists.");
                    ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                    ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                    ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
                    ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName");
                    ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");
                    
                    return View(themeLayerDetail);
                }
                
                if (themeLayerDetail.LayerTypeId != AppStaticBase.LayerTypeTabular)
                {
                    var shapeFileExtList = shapeFile.Select(item => ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Value).Select(System.IO.Path.GetExtension).ToList();
                    var jsonFileName = ContentDispositionHeaderValue.Parse(geoJsonFile[0].ContentDisposition).FileName.Value;

                    if (!(shapeFileExtList.Contains(".dbf") && shapeFileExtList.Contains(".prj") && shapeFileExtList.Contains(".shp") && shapeFileExtList.Contains(".shx") && System.IO.Path.GetExtension(jsonFileName).Contains(".json")))
                    {
                        ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                        ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                        ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
                        ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName");
                        ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");
                        ViewData["missingFileErrMsg"] = "Please upload all four shape files with extension .dbf, .prj, .shp, .shx";
                        return View(themeLayerDetail);
                    }
                    
                    //=====Error on shape file size 
                    long shapeFileSize=0;
                    int allowedSize = 100;
                    shapeFile.ForEach(x=>shapeFileSize+= x.Length);
                    if ((float)shapeFileSize / 1000000 > allowedSize)
                    {
                        ViewData["ThemeId"] = new SelectList(_context.Themes, "ThemeId", "ThemeName");
                        ViewData["SubThemeId"] = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName", themeLayerDetail.SubThemeId);
                        ViewData["LayerTypeId"] = new SelectList(_context.ThemeLayerTypes, "LayerTypeId", "LayerTypeName", themeLayerDetail.LayerTypeId);
                        ViewData["BoundaryList"] = new SelectList(_context.BoundaryInfos, "Id", "BoundaryName");
                        ViewData["LegendColorOptionList"] = new SelectList(_context.LegendColorOptions, "Id", "OptionName");
                        ViewData["missingFileErrMsg"] = $"Total size of all four shape files must be less than {allowedSize} MB.";
                        return View(themeLayerDetail);
                    }

                    var subThemeObj = _context.SubThemes
                        .Include(s => s.Themes)
                        .FirstOrDefault(s => s.SubThemeId == themeLayerDetail.SubThemeId);
                    var themePath = subThemeObj?.Themes.ThemePath;
                    var subThemePath = subThemeObj?.SubThemePath;

                    var jsonFileFinalName = themeLayerDetail.LayerName + System.IO.Path.GetExtension(jsonFileName);
                    var jsonFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}\\{jsonFileFinalName}";
                    Directory.CreateDirectory(Directory.GetParent(jsonFilePath).FullName);
                    await using var output = System.IO.File.Create(jsonFilePath);
                    await geoJsonFile[0].CopyToAsync(output);

                    foreach (var file in shapeFile)
                    {
                        var shapeFileName = themeLayerDetail.LayerName + System.IO.Path.GetExtension(file.FileName);
                        var shapeFilePath = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}\\{shapeFileName}";

                        Directory.CreateDirectory(Directory.GetParent(shapeFilePath).FullName);
                        await using var shapeOutput = System.IO.File.Create(shapeFilePath);
                        await file.CopyToAsync(shapeOutput);
                    }

                    themeLayerDetail.LayerFileName = jsonFileFinalName;
                }
                themeLayerDetail.GeneratedAt = DateTime.Now;
                themeLayerDetail.UserId = (await _userManager.GetUserAsync(User)).Id;
                themeLayerDetail.ReadStatus = false;
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
        //RMO

        //[UserAuthorization]
        public async Task<JsonResult> GetSubThemeData(int themeId)
        {
            var user =  await _userManager.GetUserAsync(User);
            var userPerComponents = _context.RoleWiseComponents
                .Where(r => r.UserRoleId == user.UserRoleId).Select(r => r.SubThemeId).ToList();
            var subThemeList =  _context.SubThemes.Where(e => e.ThemeId == themeId && userPerComponents.Contains(e.SubThemeId)).ToList();

            subThemeList.Insert(0, new SubTheme { SubThemeId = 0, SubThemeName = "--Select--" });
            return  Json(new SelectList(subThemeList, "SubThemeId", "SubThemeName"));
        }

        public JsonResult GetLayerData(int subThemeId)
        {
            var layerList = _context.ThemeLayerDetails.Where(e => e.SubThemeId == subThemeId).ToList();
            layerList.Insert(0, new ThemeLayerDetail { LayerId = 0, LayerDisplayName = "--Select--" });

            return Json(new SelectList(layerList, "LayerId", "LayerDisplayName"));
        }

        public JsonResult GetTableInfoData(int subThemeId, int boundaryId)
        {
            var tableList = _context.TableInfos.Where(e => e.SubThemeId == subThemeId && e.BoundaryId == boundaryId).ToList();
            tableList.Insert(0, new TableInfo { Id = 0, DisplayName = "--Select--" });

            return Json(new SelectList(tableList, "Id", "DisplayName"));
        }

        public JsonResult GetSubLayer(int layerId)
        {
            var getTableId = _context.ThemeLayerDetails.Where(i => i.LayerId == layerId).Select(i => i.TableInfoId).FirstOrDefault();
            if (getTableId == null)
            {
                return Json(new SelectList("", ""));
            }
            var tableColumnList = _context.TableColumnInfos.Where(e => e.TableId == getTableId && e.ColumnTypeId == 2).ToList();
            tableColumnList.Insert(0, new TableColumnInfo { Id = 0, DisplayName = "--Select--" });

            return Json(new SelectList(tableColumnList, "Id", "DisplayName"));
        }

        // GET: ThemeLayerDetails/Edit/5
        [UserAuthorization]
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

            ViewBag.DataStatusId =new SelectList(await _context.DataVerificationStates.ToListAsync(), "Id", "StateName");
            var user = await _userManager.GetUserAsync(User);
            ViewBag.isSystemAdmin = user.UserRoleId != null && user.UserRoleId == "f3b152e7-5e27-4d94-8101-5994faef8fdd" ? true : false;
            return View(themeLayerDetail);
        }

        // POST: ThemeLayerDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public async Task<IActionResult> Edit(int id, [Bind("LayerId,SubThemeId,LayerDisplayName,LayerName,LayerFileName,LayerTypeId,MainAttributeDisplayName,MainAttributeName,MainAttributeCode,FileLatName,FileLongName," +
            "LegendColorOptionId,LegendColorFieldName,LineColorCode,FillColorCode,Opacity,FillOpacity,LineWeight,BoundaryInfoId,TableInfoId,DataVerificationStateId,,GeneratedAt,UserId,ReadStatus")]
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
                        var shapeFileExtList = shapeFile.Select(item => ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Value).Select(System.IO.Path.GetExtension).ToList();
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
                            var shapeFileName = themeLayerDetail.LayerName + System.IO.Path.GetExtension(file.FileName);
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

                        var jsonFileFinalName = themeLayerDetail.LayerName + System.IO.Path.GetExtension(jsonFileName);
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
                    var sourceThemePath = await _context.ThemeLayerDetails.Include(t => t.SubThemes.Themes).Where(i => i.LayerId == id).Select(i => i.SubThemes.Themes.ThemePath).FirstOrDefaultAsync();
                    var sourceSubThemePath = await _context.ThemeLayerDetails.Include(t => t.SubThemes.Themes).Where(i => i.LayerId == id).Select(i => i.SubThemes.SubThemePath).FirstOrDefaultAsync();
                    var SubThemeId = await _context.ThemeLayerDetails.Include(t => t.SubThemes.Themes).Where(i => i.LayerId == id).Select(i => i.SubThemeId).FirstOrDefaultAsync();

                    if (SubThemeId != themeLayerDetail.SubThemeId)
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
        [UserAuthorization]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var themeLayerDetail = await _context.ThemeLayerDetails.FirstOrDefaultAsync(m => m.LayerId == id);
            if (themeLayerDetail == null)
            {
                return Json("Invalid Theme Layer");
            }
            
            //==== Delete Folders from Desired Location
            var subThemeObj = _context.SubThemes
            .Include(s => s.Themes)
            .FirstOrDefault(s => s.SubThemeId == themeLayerDetail.SubThemeId);
            _context.Remove(themeLayerDetail);
            await _context.SaveChangesAsync();
            var folderDirectory = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{subThemeObj.Themes.ThemePath?.Trim()}\\{subThemeObj.SubThemePath?.Trim()}\\{themeLayerDetail.LayerName.Trim()}";
            if(Directory.Exists(folderDirectory))
                Directory.Delete(folderDirectory,true);
            return Json("Theme Layer deleted successfully.");
        }

        public async Task<FileResult> Download(int? id)
        {


            var layerObj = _context.ThemeLayerDetails.Include(t => t.SubThemes.Themes).FirstOrDefault(m => m.LayerId == id);
            var themePath = layerObj?.SubThemes.Themes.ThemePath;
            var subThemePath = layerObj?.SubThemes.SubThemePath;
            var folderPathDirectory = $"{_hostEnvironment.WebRootPath}\\assets\\js\\map\\map_data\\{themePath?.Trim()}\\{subThemePath?.Trim()}\\{layerObj.LayerName.Trim()}";



            var webRoot = _hostEnvironment.WebRootPath;
            var fileName = layerObj.LayerName.Trim() + ".zip";
            var tempOutput = webRoot + "/zip/" + fileName;

            if (layerObj.LayerTypeId == 4)
            {
                var tableName = _context.TableInfos.Where(i => i.Id == layerObj.TableInfoId).Select(i => i.TableName).FirstOrDefault();
                IList<string> tableColumn = _context.TableColumnInfos.Where(i => i.TableId == layerObj.TableInfoId).Select(i => i.DbColumnName).ToArray();

                string columList = "";
                for (var i = 0; i < tableColumn.Count(); i++)
                {
                    columList += (i == 0 ? tableColumn[i] : "," + tableColumn[i]);
                }

                Task<DataTable> data = _context.GetAllData(tableColumn, columList, tableName);
                //var data = _context.ExecSql<>("select " + columList + " from " + tableName);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    //DataTable dt = this.GetCustomers().Tables[0];
                    DataTable dt = await data;
                    wb.Worksheets.Add(dt, "data");//.Substring(0, 20));  //Sheet Name
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", layerObj.LayerName.Trim() + ".xlsx");
                    }
                }

                //return File("", "application/zip", fileName);
            }

            using (ZipOutputStream IzipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
            {
                IzipOutputStream.SetLevel(9);
                byte[] buffer = new byte[4096];

                var files = Directory.EnumerateFiles(folderPathDirectory, "*", SearchOption.AllDirectories);
                foreach (string filePath in files)
                {
                    ZipEntry entry = new ZipEntry(System.IO.Path.GetFileName(filePath));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    IzipOutputStream.PutNextEntry(entry);

                    using (FileStream oFileStream = System.IO.File.OpenRead(filePath))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = oFileStream.Read(buffer, 0, buffer.Length);
                            IzipOutputStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                IzipOutputStream.Finish();
                IzipOutputStream.Flush();
                IzipOutputStream.Close();
            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);
            if (System.IO.File.Exists(tempOutput))
            {
                System.IO.File.Delete(tempOutput);
            }
            if (finalResult == null || !finalResult.Any())
            {
                throw new Exception(String.Format("Nothing found"));
            }
            //=======  Add Download information to the user log table
            
            var userLog = new DownloadLog()
            {
                IPAddress = HttpContext.Connection.LocalIpAddress.ToString(),
                ThemeLayerId = (int)id,
                UserId = (await _userManager.GetUserAsync(User)).Id,
                GeneratedAt = DateTime.Now
            };
            _context.Add(userLog);
            await _context.SaveChangesAsync();

            return File(finalResult, "application/zip", fileName);
        }



        // POST: ThemeLayerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
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

        public JsonResult GetNotification(int themeId)
        {
            var ThemeLayerDetails = _context.ThemeLayerDetails.Where(e => e.ReadStatus == false).ToList();

            return Json(ThemeLayerDetails);
        }


        
    }
}
