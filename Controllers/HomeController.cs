using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.ViewModels.Dashboard;
using EcdsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

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

            var dataList = _context.ThemeLayerDetails
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
                        }).ToList();

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
            return View(dashboardModel);
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
                            children = k.GroupBy(i => i.SubThemes.SubThemeName)
                                .Select(j => new
                                {
                                    name = j.Key,
                                    description = j.Select(i => i.SubThemes.SubThemePath).FirstOrDefault(),
                                    children = j.GroupBy(j => j.LayerId).
                                    Select(p => new
                                    {
                                        name = p.Select(i => i.LayerDisplayName).FirstOrDefault(),
                                        description = p.Select(i => i.LayerName).FirstOrDefault(),
                                        size = 1


                                    }).ToList()

                                }).ToList()
                        }).ToList();

            data.name = "Theme Wise Information";
            data.description = "Click cirle Box to view details and click center to back";
            data.children = dataList;

            return Json(data);

        }

        public IActionResult index()
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
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult OurPolicies()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email, [FromServices] IEmailSender emailSender)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Json("The link has been sent to the registered email address.");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);


                bool emailState = await emailSender.SendEmailAsync(new Models.ViewModels.EmailModel()
                {
                    To = email,
                    Subject = "Password Reset of ECDS platform.",
                    Msg = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                });
                if (emailState)
                {
                    return Json("The Password reset link has been sent successfully. Please check your inbox.");
                }

            }

            return Json("The link has been sent to the registered email address.");


        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmailLink(string email, [FromServices] IEmailSender _emailSender)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            bool state = await _emailSender.SendEmailAsync(new Models.ViewModels.EmailModel()
            {
                Subject = "Confirm your email",
                To = email,
                Msg = $"Please confirm your email by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
            });
            if (state)
            {
                return Json("The confirmation link has been sent successfully.");
            }

            return Json("Failed to confirm the email.");
        }


        [Authorize]
        public async Task<IActionResult> ViewProfile()
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            return View(applicationUser);  
        }
    }
}
