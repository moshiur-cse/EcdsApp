using EcdsApp.Data;
using EcdsApp.Models.SystemCommon;
using EcdsApp.Models.UserManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChatController(DataContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult DisplayChats(string receiver,string sender)
        {
            if (!(string.IsNullOrEmpty(receiver) || string.IsNullOrEmpty(receiver)))
            {
                var chats = _context.Chats
                    .Where(x=>x.Sender==sender || x.Sender == receiver)
                    .Where(y => y.Receiver == receiver || y.Receiver == sender)
                    .OrderBy(y=>y.SentAt)
                    .Select(x => new {x.Sender,x.Receiver,x.Message,x.SentAt})
                    .ToList();
                if (chats == null)
                    return Json("not found");
                return Json(chats);
            }
            return Json("error");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMsg(string msg,string receiver)
        {
            if (!(string.IsNullOrEmpty(msg)|| string.IsNullOrEmpty(receiver)))
            {
                var user=await _userManager.GetUserAsync(User);
                Chat chat = new () {
                    Sender=user.Email,
                    Receiver=receiver,
                    Message=msg,
                    SentAt=DateTime.Now
                };
                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
                return Json("success");

            }
            return Json("error");
        }

        public IActionResult PersonalChat(string email)
        {
            if (!ModelState.IsValid)
            {
               var chats = _context.Chats.Where(y => y.Sender == email).OrderByDescending(y => y.SentAt).Select(y=>new{y.Message,y.Sender,y.Receiver}).ToList();
               return Json(chats);
            }
            return Json("error");
        }
    }
}
