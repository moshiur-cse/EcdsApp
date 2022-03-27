using System;
using System.Collections.Generic;
using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EcdsApp.Models.ViewModels.UserManage;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcdsApp.Controllers.User_Manage
{
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

        public IActionResult Users()
        {
            var userList = _userManager.Users
                .Include(u => u.UserRole)
                .ToList();
            return View(userList);
        }

        public IActionResult Roles()
        {
            var roleList = _roleManager.Roles.ToList();
            return View(roleList);
        }

        public async Task<IActionResult> EditRoleInfo(string roleId, string roleName)
        {
            const string result = "Success";
            const string error = "Error";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Json(error);

            return Json(error);
        }

        [HttpGet]
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
        public IActionResult EditRoleWithAccess(string roleId)
        {
            var menuData = _context.UserPermittedContents
                .ToList()
                .Where(m => m.IsActive)
                .GroupBy(x => x.MenuName)
                .ToDictionary(x => x.Key,
                    x => x.Select(y => 
                        new MenuContent { ContentId = y.ContentId, Action = y.MenuContent, IsChecked = CheckExistingRole(y.ContentId, roleId)}).OrderBy(v => v.ContentId).ToList());
            
            var roleObj = _roleManager.Roles.FirstOrDefault(r => r.Id == roleId);
            if (roleObj == null)
                return NotFound();

            var userAccessVm = new UserAccessVm
            {
                RoleName = roleObj.Name,
                Data = menuData
            };

            var extPermComponent = new int[]{ };
            var componentList = _context.SubThemes.ToList();
            int[] arrayToBeFilled = { };
            foreach (var item in componentList)
            {
                if (DoesHaveAccessToComponent(item.SubThemeId, roleObj.Id))
                {
                    //extPermComponent.Add(item.SubThemeId.ToString());
                    //ArrayPush(ref extPermComponent, item.SubThemeId);
                    arrayToBeFilled = arrayToBeFilled.Append(item.SubThemeId).ToArray();
                }
            }

            //ViewBag.JsonObj = JsonConvert.SerializeObject(compWithSelectedValList);
            // Convert to array
            //var myArray = extPermComponent.ToArray();

            ViewBag.SelectedValue = "[1, 4, 5]";
            ViewBag.ComponentList = new SelectList(_context.SubThemes, "SubThemeId", "SubThemeName");
            return View(userAccessVm);
        }

        public static void ArrayPush<T>(ref T[] table, object value)
        {
            Array.Resize(ref table, table.Length + 1); // Resizing the array for the cloned length (+-) (+1)
            table.SetValue(value, table.Length - 1); // Setting the value for the new element
        }

        public bool DoesHaveAccessToComponent(int componentId, string roleId)
        {
            var returnObj = _context.RoleWiseComponents.FirstOrDefault(r => r.UserRoleId == roleId && r.SubThemeId == componentId);
            return returnObj != null;
        }

        public async Task<IActionResult> SaveRoleWiseAccess(UserAccessVm formData)
        {
            const string result = "Success";
            const string error = "Error";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Json(error);

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
                        Id = (_context.RoleWiseComponents.Max(r => (int?) r.Id) ?? 0) + 1,
                        UserRoleId = roleId,
                        SubThemeId = Convert.ToInt32(compItem)
                    };
                    await _context.RoleWiseComponents.AddAsync(roleWiseCompModel);
                    var addRoleWiseCompResult = await _context.SaveChangesAsync();
                    if (addRoleWiseCompResult != 1)
                        return Json(error);
                }

                foreach (var contentItem in formData.ContentId)
                {
                    var roleWiseContentModel = new RoleWisePermittedContent
                    {
                        Id = (_context.RoleWisePermittedContents.Max(r => (int?) r.Id) ?? 0) + 1,
                        UserRoleId = roleId,
                        ContentId = Convert.ToInt32(contentItem)
                    };

                    await _context.RoleWisePermittedContents.AddAsync(roleWiseContentModel);
                    var addRoleWiseContResult = await _context.SaveChangesAsync();
                    if (addRoleWiseContResult != 1)
                        return Json(error);
                }

                await transaction.CommitAsync();
                return Json(result);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ViewBag.ErrorMsg = ex;
                return Json(error);
            }

            //return Json(result);
        }

        public bool CheckExistingRole(int contentId, string roleId)
        {
            var data = _context.RoleWisePermittedContents.FirstOrDefault(r => r.UserRoleId == roleId && r.ContentId == contentId);
            return data != null;
        }
    }
}
