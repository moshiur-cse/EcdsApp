using DRIPWebApp.Data;
using EcdsApp.Data;
using EcdsApp.Models;
using EcdsApp.Models.HitCountAndLogModels;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.ViewModels.Dashboard;
using EcdsApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Core.ExcelPackage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Security.Claims;


namespace EcdsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

            public HomeController(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; 
            _userManager = userManager;
        }
        public async Task<IActionResult> Dashboard()
        {
            //try
            //{
                var population = _context.DistrictWisePopulations
                    .Include(u => u.District.Division)
                    .OrderBy(u => u.Id)
                    .ToList();
                ViewBag.LayerCount = _context.ThemeLayerDetails.Count();
                var layerInfoList = _context.ThemeLayerDetails
                    .Include(t => t.SubThemes.Themes)
                    .Include(t => t.ThemeLayerTypes)
                    .Include(t => t.BoundaryInfo)
                    .Include(t => t.TableInfo)
                    .OrderByDescending(t => t.LayerId).Take(5)
                    .ToList();
                var layerData = await _context.DistrictWisePoverties.ToListAsync();
                var legendData = await _context.LayerLegendColors.Where(i => i.LayerId == 11).ToListAsync();
                ViewBag.DistrictList = new SelectList(_context.AdminBoundaryDistricts.OrderBy(i => i.DistrictName), "DistrictGeoCode", "DistrictName");



                ChartDataVm chartData = new ChartDataVm();

                var dataList = await _context.ThemeLayerDetails
                           .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                            .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                            .Select(k => new
                            {
                                name = k.Key,
                                children = k.GroupBy(i => i.SubThemes.SubThemeName)
                                    .Select(j => new
                                    {
                                        name = j.Key,
                                        children = j.GroupBy(j => j.LayerId).
                                        Select(p => new
                                        {
                                            name = p.Select(i => i.LayerDisplayName).FirstOrDefault(),
                                            value = p.Key


                                        }).ToList()

                                    }).ToList()
                            }).ToDynamicListAsync();

                chartData.name = "dataList";
                chartData.children = dataList;

                var dashboardModel = new DashboardVm
                {
                    DistrictWisePopulations = population,
                    ThemeLayerDetails = layerInfoList,
                    DistrictWisePoverties = layerData,
                    LayerLegendColors = legendData,
                    ChartDataVms = chartData
                };


                hitCount();
                return View(dashboardModel);
            //}
            //catch (Exception e)
            //{
                
            //    return new RedirectToRouteResult(new { action = "UnAuthorizeActionResult", controller = "UserAccessManage", area = "" });
            //}
        }


        void   hitCount()
        {
            //=====Insert Server Hit Info in our system.
            var ipaddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var countryOfOrigin = "";
            //try
            //{
            //     countryOfOrigin = GetCountryOfOrginFromIPAddress(ipaddress);
            //}
            //catch {
            //    countryOfOrigin = "";
            //}
            var serverRequest = new ServerHitInfo()
            {
                IPAddress = ipaddress,
                RequestedAt = DateTime.Now,
                CountryOfOrigin = countryOfOrigin
            };
            _context.Add(serverRequest);
            _context.SaveChangesAsync();


        }
        public JsonResult Data()
        {


            ChartDataVm data = new ChartDataVm();

            var dataList = _context.ThemeLayerDetails
                       .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                        .GroupBy(model => model.SubThemes.Themes.ThemeName).AsQueryable().ToList()
                        .Select(k => new
                        {
                            name = k.Key,
                            description = k.Select(i => i.SubThemes.Themes.ThemePath).FirstOrDefault(),
                            color = k.Select(i => i.SubThemes.Themes.ThemeColor).FirstOrDefault(),
                            children = k.GroupBy(i => i.SubThemes.SubThemeName)
                                .Select(j => new
                                {
                                    name = j.Key,
                                    description = j.Select(i => i.SubThemes.SubThemePath).FirstOrDefault(),
                                    color = j.Select(i => i.SubThemes.Themes.ThemeColor).FirstOrDefault(),
                                    children = j.GroupBy(j => j.LayerId).
                                    Select(p => new
                                    {
                                        name = p.Select(i => i.LayerDisplayName).FirstOrDefault(),
                                        description = p.Select(i => i.LayerName).FirstOrDefault(),
                                        size = p.Count(),
                                        color = p.Select(i => i.SubThemes.Themes.ThemeColor).FirstOrDefault(),
                                    }).ToList()

                                }).ToList()
                        }).ToList();

            data.name = "Theme Wise Information \n (Click title to view details and click center to back)";
            data.description = "Click title to view details and click center to back";
            data.color = "#007500";
            data.children = dataList;
            return Json(data);
        }

        public JsonResult Data1()
        {



            var data = _context.ThemeLayerDetails
                       .Include(s => s.SubThemes.Themes).AsQueryable().ToList()
                        .GroupBy(model => model.SubThemes.Themes.ThemeId).AsQueryable().ToList()
                        .Select(k => new
                        {
                            id = k.Key.ToString(),
                            name = k.Select(i => i.SubThemes.Themes.ThemeName).FirstOrDefault(),
                            parent = "0",
                            group1 = k.GroupBy(i => i.SubThemes.SubThemeId)
                                .Select(j => new
                                {
                                    id = k.Key + "." + j.Key,
                                    name = j.Select(i => i.SubThemes.SubThemeName).FirstOrDefault(),
                                    parent = k.Key.ToString(),
                                    group2 = j.GroupBy(j => j.LayerId).
                                    Select(p => new
                                    {
                                        name = p.Select(i => i.LayerDisplayName).FirstOrDefault(),
                                        parent = k.Key + "." + j.Key,
                                        value = p.Count()
                                    }).ToList()
                                }).ToList()
                        }).ToList();




            return Json(data);
        }

        public IActionResult index()
        {
            return View();
        }


        public IActionResult ecds_main()
        {
            return View();
        }
        public JsonResult GetChartData(string[] distCodeList)
        {

            var data = _context.DistrictWisePopulations.Include(d => d.District).Where(i => distCodeList.Contains(i.DistrictGeoCode)).
                Select(j => new
                {
                    distName = j.District.DistrictName,
                    male = j.Male,
                    female = j.Female,
                    total = j.Total
                }).OrderBy(i => i.total).ToList();

            return Json(data);
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}
        //public IActionResult Register()
        //{
        //    return View();
        //}

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(Message message, [FromServices] IEmailSender _emailSender)
        {
            if (ModelState.IsValid)
            {
                //====try to send email

                bool state = await _emailSender.SendEmailAsync(new Models.ViewModels.EmailModel()
                {
                    Subject = "Message from ECDS Contact form",
                    To = Credentials.DefaultEmailReceiver,
                    Msg = $"Dear Admin,<br/><br/>{message.Msg}<br/><br/>Best Wishes<br/>Name: {message.FullName}<br/>email: {message.Email}"
                });

                if (state)
                {
                    message.ReplyStatusId = 1;
                    message.CreatedAt = DateTime.Now;
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();
                    ViewData["Message"] = "The message has been sent successfully.";
                }
            }
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult OurPolicies()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                string tkn = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
                var result = await _userManager.ConfirmEmailAsync(user, tkn);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Success";
                }
            }
            else
            {
                ViewBag.Message = "Failed";
            }

            return View();
        }

        private string GetCountryOfOrginFromIPAddress(string ipaddress)
        {
            //string url = "https://ipapi.co/" +ipaddress+ "/country/";
            //string url = "https://ipapi.co/" +"45.248.151.59"+ "/country/";
            if (ipaddress == "127.0.0.1" || ipaddress == "::1")
                return null;
            string url = "http://ip-api.com/json/" + ipaddress;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var reader = new System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.ASCII);
            string countryOfOrigin = reader.ReadToEnd();
            countryOfOrigin = (countryOfOrigin.Split("country").ToList())[1].Substring(3);
            countryOfOrigin = countryOfOrigin.Remove(countryOfOrigin.Length - 3);

            return countryOfOrigin;
        }

        [Authorize]
        public IActionResult UploadExcelData()
        {
            ViewBag.message = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        public  IActionResult Upload(IFormFile fileUpload)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string username = "";
            string userId = "";
            if (identity != null)
            {
                // Get the user's username
                 username = identity.FindFirst(ClaimTypes.Name)?.Value;

                // You can also access other claims if needed
                string email = identity.FindFirst(ClaimTypes.Email)?.Value;
                userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Do something with the user's information
            }
            else
            {
                // User is not authenticated
                // Handle accordingly
            }



           
            if (fileUpload != null && fileUpload.Length > 0)
            {
                try
                {
                    // Specify the folder where you want to save the file
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                    // Create the folder if it doesn't exist
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name to avoid overwriting existing files
                    string uniqueFileName = username +"_"+ Guid.NewGuid().ToString() + "_" + fileUpload.FileName;

                    // Combine the folder path and file name to get the full path
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the uploaded file to the specified path
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        fileUpload.CopyTo(stream);
                    }

                    ViewBag.message = "Upload Successfully";
                    return RedirectToAction("UploadExcelData"); // Redirect back to the upload page
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                // Handle case when no file is uploaded
                return BadRequest("No file uploaded.");
            }
        }


    }
}
