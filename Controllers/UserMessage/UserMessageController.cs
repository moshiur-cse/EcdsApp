using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcdsApp.Controllers.UserMessage
{
    public class UserMessageController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserMessageController(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Messages.Include(x=>x.Status).ToListAsync();
            return View(data);
        }

    }
}
