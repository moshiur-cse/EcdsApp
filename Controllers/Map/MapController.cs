using EcdsApp.Data;
using EcdsApp.Models.ViewModels.Map;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcdsApp.Models;

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

            ViewBag.LayerInfo = _context.ThemeLayerDetails
                .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                .Select(k => new ThemeList
                {
                    themeName = k.Key,
                    subThemeList=k.GroupBy(i => i.SubThemes.SubThemeName)
                        .Select(j => new SubThemeList
                        {
                            subThemeName = j.Key,                           
                            layerPathList = j.Select(j=>j.LayerPath).ToList(),
                            layerIdList= j.Select(j => j.LayerId).ToList(),
                            layerNameList= j.Select(j => j.LayerName).ToList(),
                            layerTypeIdList = j.Select(j => j.LayerTypeId).ToList(),
                            tableIdList= j.Select(j => j.TableInfoId).ToList()

                        }).ToList()


                }).ToList();
           
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
                            layerPathList = j.Select(j => j.LayerPath).ToList(),
                            layerIdList = j.Select(j => j.LayerId).ToList(),
                            layerNameList = j.Select(j => j.LayerName).ToList(),
                            layerTypeIdList = j.Select(j=>j.LayerTypeId).ToList()

                        }).ToList()


                }).ToList();

            return View();
        }

        [HttpPost]
        public JsonResult GetLayerInformation(int layer_id)
        {
           
                var data = _context.ThemeLayerDetails.Include(s => s.SubThemes.Themes)
                    .Where(s => s.LayerId == layer_id)
                    .Select(sd => new
                    {
                        
                        layerId=sd.LayerId,
                        layerName=sd.LayerName,
                        themePath = sd.SubThemes.Themes.ThemePath,
                        subThemePath = sd.SubThemes.SubThemePath,
                        layerPath = sd.LayerPath,
                        layerFileName = sd.LayerFileName,
                       
                        mainAttDisName = sd.MainAttributeDisplayName,
                        mainAtt = sd.MainAttributeName,
                        mainAttCode=sd.MainAttributeCode,

                        firstAttDisName = sd.FirstAttributeDisplayName,
                        firstAtt = sd.FirstAttributeName,
                        firstAttCode = sd.FirstAttributeCode,

                        secondAttDisName = sd.SecondAttributeDisplayName,
                        secondAtt = sd.SecondAttributeName,
                        secondAttCode = sd.SecondAttributeCode,

                        thirdAttDisName = sd.ThirdAttributeDisplayName,
                        thirdAtt = sd.ThirdAttributeName,
                        thirdAttCode = sd.ThirdAttributeCode,

                        cLat = sd.FileLatName,
                        cLong = sd.FileLongName,

                        layerTypeId = sd.LayerTypeId,
                        isLegendColor=sd.IsLegendColor,
                        legendcolorField=sd.LegendColorFieldName,


                        fillColorCode = sd.FillColorCode,
                        lineColorCode = sd.LineColorCode,
                        opacity = sd.Opacity,
                        fillOpacity = sd.FillOpacity,
                        lineWeight = sd.LineWeight,

                        tableId =sd.TableInfoId,
                        boundaryId = sd.BoundaryInfoId,
                        
                    }).FirstOrDefault();
                return Json(data);

        }

        
       [HttpPost]
       public JsonResult GetTbleColumnList(int tableId)
        {
            var dataList = _context.TableColumnInfos
                .Where(sd => sd.TableId==tableId)
                .Select(sd => new { sd.DbColumnName, sd.DisplayName })
                .OrderBy(sd => sd.DisplayName).ToList();

            return Json(new SelectList(dataList, "DbColumnName", "DisplayName"));
        }
        
       [HttpPost]
       public JsonResult GetMapBindData(int tableId, string columnName,int boundaryId)
        {
            string tableName = _context.TableInfos.Where(i => i.Id == tableId).Select(i => i.TableName).FirstOrDefault();

            var geoCodeColumnName = "";
            if (boundaryId == 1)
            {
                geoCodeColumnName = "";
            }
            else if (boundaryId == 2)
            {
                geoCodeColumnName = "";
            }
            else if (boundaryId == 3) 
            {
                geoCodeColumnName = "upz_geo_code";
            }

            string columList = geoCodeColumnName + "," + columnName;

            //var data = GetData(columList, tableName);

            var data = "";
           
            return Json(data);

        }

        [HttpPost]
       public JsonResult GetUserDefinedLegendInfo(int layer_id)
        {
            var data = _context.LayerLegendColors.Where(s => s.LayerId == layer_id).Select(sd => new
            {
                attCode=sd.LayerMainAttribureValue,
                attName = sd.LayerLegendDisplayName,
                colorCode =sd.LayerLegendColorCode,
               

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
        public  JsonResult GetUpazilaInfoes()
        {
            var upzList =  _context.AdminBoundaryUpazilas.OrderBy(sd => sd.UpazilaGeoCode).ToList();
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
    }
}
