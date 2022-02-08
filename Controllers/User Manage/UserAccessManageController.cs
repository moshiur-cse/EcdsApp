using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
    }
}
