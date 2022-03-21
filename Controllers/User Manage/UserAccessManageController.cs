using System.Collections.Generic;
using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EcdsApp.Models.ThemeModels;
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

            var compList = _context.SubThemes.ToList();
            compList.Insert(0, new SubTheme{ SubThemeId = 0, SubThemeName = "All Components"});
            ViewBag.ComponentList = new SelectList(compList, "SubThemeId", "SubThemeName", 0);

            return View(userAccessVm);
        }

        public async Task<IActionResult> SaveRoleWiseAccess(UserAccessVm formData)
        {
            var result = "Success! Process Complete!";
            var error = "Error!";

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Json(error);

            return Json(result);
        }

        public bool CheckExistingRole(int contentId, string roleId)
        {
            var data = _context.RoleWisePermittedContents.FirstOrDefault(r => r.UserRoleId == roleId && r.ContentId == contentId);
            return data != null;
        }
    }
}
