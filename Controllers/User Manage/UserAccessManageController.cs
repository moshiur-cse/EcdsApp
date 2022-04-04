using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.ViewModels.UserManage;
using EcdsApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.User_Manage
{
    [Authorize]
    public class UserAccessManageController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAccessManageController(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
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
    }
}
