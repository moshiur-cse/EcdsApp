using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.ViewModels.UserManage;
using EcdsApp.Security;
using EcdsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.User_Manage
{
    [Authorize]
    public class UserAccessManageController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserAccessManageController(DataContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [UserAuthorization]
        public IActionResult Users()
        {
            var userList = _userManager.Users
                .Include(u => u.UserRole)
                .ToList();

            ViewBag.RoleList = new SelectList(_roleManager.Roles, "Id", "Name");
            return View(userList);
        }

        [HttpPost]
        [UserAuthorization]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleId)
        {
            const string success = "Success";
            const string error = "Error";

            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
                return Json(error);

            var roleToBeAssignUser = await _userManager.FindByIdAsync(userId);
            if (roleToBeAssignUser == null)
                return Json(error);

            roleToBeAssignUser.UserRoleId = roleId;
            var result = await _userManager.UpdateAsync(roleToBeAssignUser);
            //var result = await _userManager.AddToRoleAsync(roleToBeAssignUser, roleId);

            return Json(result.Succeeded ? success : error);
        }

        [HttpGet]
        [UserAuthorization]
        public IActionResult Roles()
        {
            var roleList = _roleManager.Roles.ToList();
            return View(roleList);
        }

        [UserAuthorization]
        public async Task<IActionResult> EditRoleInfo(string roleId, string roleName)
        {
            const string success = "Success";
            const string error = "Error";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Json(error);

            try
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                role.Name = roleName;
                var roleEditResult = await _roleManager.UpdateAsync(role);

                return Json(roleEditResult.Succeeded ? success : error);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex;
                return Json(error);
            }
        }

        [HttpGet]
        [UserAuthorization]
        public IActionResult AddRoleWithAccess()
        {
            var menuData = _context.UserPermittedContents
                .ToList()
                .Where(m => m.IsActive)
                .GroupBy(x => x.MenuName)
                .ToDictionary(x => x.Key,
                    x => x.Select(y =>
                        new MenuContent { ContentId = y.ContentId, Action = y.MenuContent }).OrderBy(v => v.ContentId).ToList());

            var userAccessVm = new UserAccessVm
            {
                Data = menuData
            };

            ViewBag.ComponentList = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName");
            return View(userAccessVm);
        }

        [HttpGet]
        [UserAuthorization]
        public IActionResult EditRoleWithAccess(string roleId)
        {
            var menuData = _context.UserPermittedContents
                .ToList()
                .Where(m => m.IsActive)
                .GroupBy(x => x.MenuName)
                .ToDictionary(x => x.Key,
                    x => x.Select(y =>
                        new MenuContent { ContentId = y.ContentId, Action = y.MenuContent, IsChecked = CheckExistingRole(y.ContentId, roleId) }).OrderBy(v => v.ContentId).ToList());

            var roleObj = _roleManager.Roles.FirstOrDefault(r => r.Id == roleId);
            if (roleObj == null)
                return NotFound();

            var userAccessVm = new UserAccessVm
            {
                RoleName = roleObj.Name,
                Data = menuData
            };

            var componentList = _context.SubThemes.ToList();
            int[] extPermComponent = { };
            foreach (var item in componentList)
            {
                if (DoesHaveAccessToComponent(item.SubThemeId, roleObj.Id))
                {
                    extPermComponent = extPermComponent.Append(item.SubThemeId).ToArray();
                }
            }

            var selectedValString = string.Join(", ", extPermComponent);
            ViewBag.SelectedValue = "[" + selectedValString + "]";
            ViewBag.ComponentList = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName");
            return View(userAccessVm);
        }

        public bool DoesHaveAccessToComponent(int componentId, string roleId)
        {
            var returnObj = _context.RoleWiseComponents.FirstOrDefault(r => r.UserRoleId == roleId && r.SubThemeId == componentId);
            return returnObj != null;
        }

        [UserAuthorization]
        public async Task<IActionResult> SaveRoleWiseAccess(UserAccessVm formData)
        {
            const string success = "Success";
            const string error = "Error";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Json(error);

            if (string.IsNullOrEmpty(formData.ActionMode))
                return Json(error);

            if (formData.ActionMode == "add")
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var identityRole = new IdentityRole(formData.RoleName);
                    var roleAddResult = await _roleManager.CreateAsync(identityRole);
                    if (!roleAddResult.Succeeded)
                        return Json(error);

                    var roleId = identityRole.Id;
                    foreach (var compItem in formData.ComponentArray)
                    {
                        var roleWiseCompModel = new RoleWiseComponent
                        {
                            Id = (_context.RoleWiseComponents.Max(r => (int?)r.Id) ?? 0) + 1,
                            UserRoleId = roleId,
                            SubThemeId = Convert.ToInt32(compItem)
                        };
                        await _context.RoleWiseComponents.AddAsync(roleWiseCompModel);
                        var addRoleWiseCompResult = await _context.SaveChangesAsync();
                        if (addRoleWiseCompResult <= 0)
                            return Json(error);
                    }

                    foreach (var contentItem in formData.ContentArray)
                    {
                        var roleWiseContentModel = new RoleWisePermittedContent
                        {
                            Id = (_context.RoleWisePermittedContents.Max(r => (int?)r.Id) ?? 0) + 1,
                            UserRoleId = roleId,
                            ContentId = Convert.ToInt32(contentItem)
                        };

                        await _context.RoleWisePermittedContents.AddAsync(roleWiseContentModel);
                        var addRoleWiseContResult = await _context.SaveChangesAsync();
                        if (addRoleWiseContResult <= 0)
                            return Json(error);
                    }

                    await transaction.CommitAsync();
                    return Json(success);

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ViewBag.ErrorMsg = ex;
                    return Json(error);
                }

            }
            else
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var roleObj = await _roleManager.FindByNameAsync(formData.RoleName);

                    var roleWiseExistingCompList = _context.RoleWiseComponents.Where(r => r.UserRoleId == roleObj.Id).ToList();
                    foreach (var compItem in roleWiseExistingCompList)
                    {
                        _context.RoleWiseComponents.Remove(compItem);
                    }
                    foreach (var compItem in formData.ComponentArray)
                    {
                        var roleWiseCompModel = new RoleWiseComponent
                        {
                            Id = (_context.RoleWiseComponents.Max(r => (int?)r.Id) ?? 0) + 1,
                            UserRoleId = roleObj.Id,
                            SubThemeId = Convert.ToInt32(compItem)
                        };
                        await _context.RoleWiseComponents.AddAsync(roleWiseCompModel);
                        var addRoleWiseCompResult = await _context.SaveChangesAsync();
                        if (addRoleWiseCompResult <= 0)
                            return Json(error);
                    }

                    var roleWiseExistingContList = _context.RoleWisePermittedContents.Where(r => r.UserRoleId == roleObj.Id);
                    foreach (var contItem in roleWiseExistingContList)
                    {
                        _context.RoleWisePermittedContents.Remove(contItem);
                    }

                    foreach (var contItem in formData.ContentArray)
                    {
                        var roleWiseContentModel = new RoleWisePermittedContent
                        {
                            Id = (_context.RoleWisePermittedContents.Max(r => (int?)r.Id) ?? 0) + 1,
                            UserRoleId = roleObj.Id,
                            ContentId = Convert.ToInt32(contItem)
                        };

                        await _context.RoleWisePermittedContents.AddAsync(roleWiseContentModel);
                        var addRoleWiseContResult = await _context.SaveChangesAsync();
                        if (addRoleWiseContResult <= 0)
                            return Json(error);
                    }

                    await transaction.CommitAsync();
                    return Json(success);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    ViewBag.ErrorMsg = ex;
                    return Json(error);
                }
            }

            //return Json(result);
        }

        public bool CheckExistingRole(int contentId, string roleId)
        {
            var data = _context.RoleWisePermittedContents.FirstOrDefault(r => r.UserRoleId == roleId && r.ContentId == contentId);
            return data != null;
        }

        public IActionResult UnAuthorizeActionResult()
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
        [AllowAnonymous]
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
        public async Task<IActionResult> ViewProfile(string id)
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            if (applicationUser == null)
            {
                applicationUser = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            }
            if (id != null)
            {
                return View(await _userManager.FindByIdAsync(id));
            }
            return View(applicationUser);
        }

        [Authorize]
        public async Task<IActionResult> UpdateProfile(string userid)
        {
            var userProfile = await _userManager.FindByIdAsync(userid);
            if (userProfile == null)
                return NotFound();
            return View(userProfile);
        }

        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return Json("Succeeded");
            }
            else
            {
                var errmessg = "The User " + id + " not found";
                return Json(errmessg);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(ApplicationUser appUser, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(appUser.Id);

                if (image != null)
                {
                    if (user.ProfilePic != null)
                    {
                        FileInfo f = new FileInfo(user.ProfilePic);
                        if (f.Exists)//=====check if file exists or not  
                        {
                            f.Delete();
                        }
                    }

                    var supportedTypes = new[] { "jpg", "png" };
                    var fileExt = Path.GetExtension(image.FileName).Substring(1).ToLower();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ViewData["msg"] = "Choose a jpg or png image.";
                        return View(appUser);
                    }
                    else
                    {

                        var fileToUpload = user.UserName + Path.GetExtension(image.FileName);
                        var folderPathDirectory = $"{_hostEnvironment.WebRootPath}\\assets\\profile_pics\\{fileToUpload}";

                        using (var fileStream = new FileStream(folderPathDirectory, FileMode.Create, FileAccess.Write))
                        {
                            image.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                        appUser.ProfilePic = fileToUpload;
                    }


                }

                user.FirstName = appUser.FirstName;
                user.LastName = appUser.LastName;
                user.Address = appUser.Address;
                user.MobileNo = appUser.MobileNo;
                user.Designation = appUser.Designation;
                user.DateOfBirth = appUser.DateOfBirth;
                user.Organization = appUser.Organization;
                user.ProfilePic = appUser.ProfilePic;
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewProfile");
            }
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            return View(applicationUser);
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "UserAccessManage");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {

            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToPage("/Identity/Account/Login");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
                return View(userInfo);
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };

                var registeredUser = _userManager.FindByEmailAsync(user.Email);
                if (registeredUser.Result == null)
                {
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Dashboard", "Home");
                        }
                    }
                }
                else
                {
                    //await _signInManager.SignInAsync(user, false);
                    //return RedirectToAction("Dashboard","Home");
                    //var status=_signInManager.IsSignedIn(info.Principal);
                    //if(status)
                    //    await _signInManager.SignOutAsync();
                    string msg = "The user already exits in the system. Please use existing credentials.";
                    string url = "/Identity/Account/Login?message=" + msg;
                    return LocalRedirect(url);

                }

                return RedirectToPage("/Identity/Account/Login");
            }
        }

    }
}
