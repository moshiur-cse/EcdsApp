using EcdsApp.Data;
using EcdsApp.Models.ViewModels.Map;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.Map
{
    public class MapController : Controller
    {
        private readonly DataContext _context;

        public MapController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> BasicMap()
        {
            //var dataContext = _context.ThemeLayerDetails.Include(s => s.SubThemes.Themes);
            ViewBag.DivList = await _context.AdminBoundaryDivisions.ToListAsync();
            ViewBag.DistList = await _context.AdminBoundaryDistricts.ToListAsync();
            ViewBag.UpazList = await _context.AdminBoundaryUpazilas.ToListAsync();

            ViewBag.LayerInfo = _context.ThemeLayerDetails.Where(i => i.DataVerificationStateId == 1)
                .Include(s => s.SubThemes.Themes).OrderBy(model => model.SortingOrder).AsQueryable().ToList()
                .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                .Select(k => new ThemeList
                {
                    themeName = k.Key,
                    subThemeList = k.GroupBy(i => i.SubThemes.SubThemeName)
                        .Select(j => new SubThemeList
                        {
                            subThemeName = j.Key,
                            layerNameList = j.Select(j => j.LayerName).ToList(), //Update
                            layerIdList = j.Select(j => j.LayerId).ToList(),
                            layerDisplayNameList = j.Select(j => j.LayerDisplayName).ToList(), //Update
                            layerTypeIdList = j.Select(j => j.LayerTypeId).ToList(),
                            tableIdList = j.Select(j => j.TableInfoId).ToList()

                        }).OrderBy(i => i.subThemeName).ToList()
                }).OrderBy(i => i.themeName).ToList();

            //return Json(themeList);

            //ViewBag.LayerInfoes = await dataContext.ToListAsync();


            return View();
        }

        public async Task<IActionResult> MapComparison()
        {
            //var dataContext = _context.ThemeLayerDetails.Include(s => s.SubThemes.Themes);
            ViewBag.DivList = await _context.AdminBoundaryDivisions.ToListAsync();
            ViewBag.DistList = await _context.AdminBoundaryDistricts.ToListAsync();
            ViewBag.UpazList = await _context.AdminBoundaryUpazilas.ToListAsync();

            ViewBag.LayerInfo = _context.ThemeLayerDetails.Where(i => i.LayerTypeId == 4 && i.DataVerificationStateId != 3)
                .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                .Select(k => new ThemeList
                {
                    themeName = k.Key,
                    subThemeList = k.GroupBy(i => i.SubThemes.SubThemeName)
                        .Select(j => new SubThemeList
                        {
                            subThemeName = j.Key,
                            layerNameList = j.Select(j => j.LayerName).ToList(), //Update
                            layerIdList = j.Select(j => j.LayerId).ToList(),
                            layerDisplayNameList = j.Select(j => j.LayerDisplayName).ToList(), //Update
                            layerTypeIdList = j.Select(j => j.LayerTypeId).ToList(),
                            tableIdList = j.Select(j => j.TableInfoId).ToList()

                        }).OrderBy(i => i.subThemeName).ToList()
                }).OrderBy(i => i.themeName).ToList();

            //return Json(themeList);

            //ViewBag.LayerInfoes = await dataContext.ToListAsync();


            return View();
        }


        public IActionResult ThematicMap()
        {
            return View();
        }

        public async Task<IActionResult> RasterMap()
        {
            ViewBag.DivList = await _context.AdminBoundaryDivisions.ToListAsync();
            ViewBag.DistList = await _context.AdminBoundaryDistricts.ToListAsync();
            ViewBag.UpazList = await _context.AdminBoundaryUpazilas.ToListAsync();

            ViewBag.LayerInfo = _context.ThemeLayerDetails
                 .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                 .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                 .Select(k => new ThemeList
                 {
                     themeName = k.Key,
                     subThemeList = k.GroupBy(i => i.SubThemes.SubThemeName)
                         .Select(j => new SubThemeList
                         {
                             subThemeName = j.Key,
                             layerNameList = j.Select(j => j.LayerName).ToList(), //Update
                             layerIdList = j.Select(j => j.LayerId).ToList(),
                             layerDisplayNameList = j.Select(j => j.LayerDisplayName).ToList(), //Update
                             layerTypeIdList = j.Select(j => j.LayerTypeId).ToList(),
                             tableIdList = j.Select(j => j.TableInfoId).ToList()

                         }).ToList()


                 }).ToList();

            return View();
        }

        [HttpPost]
        public JsonResult GetLayerInformation(int layer_id)
        {

            var data = _context.ThemeLayerDetails.Include(s => s.SubThemes.Themes).Include(s => s.BoundaryInfo)
                .Where(s => s.LayerId == layer_id)
                .Select(sd => new
                {

                    layerId = sd.LayerId,
                    layerName = sd.LayerDisplayName, //Update
                    themePath = sd.SubThemes.Themes.ThemePath,
                    subThemePath = sd.SubThemes.SubThemePath,
                    layerPath = sd.LayerName, ///Update
                    layerFileName = sd.LayerFileName,

                    mainAttDisName = sd.MainAttributeDisplayName,
                    mainAtt = sd.MainAttributeName,
                    mainAttCode = sd.MainAttributeCode,

                    cLat = sd.FileLatName,
                    cLong = sd.FileLongName,

                    layerTypeId = sd.LayerTypeId,
                    isLegendColor = sd.LegendColorOptionId,
                    legendcolorField = sd.LegendColorFieldName,


                    fillColorCode = sd.FillColorCode,
                    lineColorCode = sd.LineColorCode,
                    lineOpacity = sd.Opacity,
                    fillOpacity = sd.FillOpacity,
                    lineWeight = sd.LineWeight,

                    tableId = sd.TableInfoId,
                    boundaryId = sd.BoundaryInfoId,
                    boundaryPath = sd.BoundaryInfo.BoundaryPath,
                    boundaryGeoCodeColumnName = sd.BoundaryInfo.AttributeName,
                    boundaryGeoNameColumnName = sd.BoundaryInfo.AttributeValueName,

                }).FirstOrDefault();
            return Json(data);

        }


        [HttpPost]
        public JsonResult GetTbleColumnList(int tableId)
        {
            var dataList = _context.TableColumnInfos
                .Where(sd => sd.TableId == tableId && sd.ColumnTypeId == 2)
                .Select(sd => new { sd.DbColumnName, sd.DisplayName })
                .OrderBy(sd => sd.DisplayName).ToList();

            return Json(new SelectList(dataList, "DbColumnName", "DisplayName"));
        }

        [HttpPost]
        public JsonResult GetMapBindData(int tableId, string columnName, int boundaryId)
        {
            string tableName = _context.TableInfos.Where(i => i.Id == tableId).Select(i => i.TableName).FirstOrDefault();
            string geoCodeColumnName = _context.TableColumnInfos.Where(i => i.TableId == tableId && i.ColumnTypeId == 1).Select(i => i.DbColumnName).FirstOrDefault();

            string columList = geoCodeColumnName + "," + columnName;

            var data = _context.GetTabularData(columList, tableName);

            return data;

        }

        [HttpPost]
        public JsonResult GetUserDefinedLegendInfo(int layer_id, string columnName)
        {
            //s.LegendColumnName == columnName &&
            var data = _context.LayerLegendColors.Where(s => s.LayerId == layer_id).Select(sd => new
            {
                attCode = sd.LayerMainAttribureValue,
                attName = sd.LayerLegendDisplayName,
                colorCode = sd.LayerLegendColorCode,
                iconSize = sd.IconSize,
                iconPath = sd.IconPath


            }).ToList();
            return Json(data);

        }

        [HttpPost]
        public JsonResult GetDistrictList(string[] divisionList)
        {
            var distList = _context.AdminBoundaryDistricts
                .Where(sd => divisionList.Contains(sd.DivisionGeoCode))
                .Select(sd => new { sd.DistrictGeoCode, sd.DistrictName })
                .OrderBy(sd => sd.DistrictGeoCode).ToList();

            return Json(new SelectList(distList, "DistrictGeoCode", "DistrictName"));
        }


        [HttpPost]
        public JsonResult GetUpazilaList(string[] upazilaList)
        {
            var upzList = _context.AdminBoundaryUpazilas
                .Where(sd => upazilaList.Contains(sd.DistrictGeoCode))
                .Select(sd => new { sd.UpazilaGeoCode, sd.UpazilaName })
                .OrderBy(sd => sd.UpazilaGeoCode).ToList();

            return Json(new SelectList(upzList, "UpazilaGeoCode", "UpazilaName"));
        }


        [HttpPost]
        public JsonResult GetUpazilaInfoes()
        {
            var upzList = _context.AdminBoundaryUpazilas.OrderBy(sd => sd.UpazilaGeoCode).ToList();
            return Json(upzList);
        }

        public IActionResult MapComponents(string adminCodeForGetResult, int indicatorId, int isShowLayout = 0, int isShowAction = 0)
        {
            ViewBag.IndicatorId = indicatorId;
            ViewBag.AdminCode = adminCodeForGetResult;
            ViewBag.IsShowLayout = isShowLayout;
            ViewBag.IsShowAction = isShowAction;
            return View("MapComponents");
        }
        public IActionResult GetMetadata(int layerId)
        {
            ViewBag.LayerId = layerId;
            ViewBag.IsShowLayout = 0;
            ViewBag.IsShowAction = 0;
            return View("MetadataAndBundle");
        }



    }
}
