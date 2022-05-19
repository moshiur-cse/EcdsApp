using EcdsApp.Data;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.UserMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            var data = await _context.Messages.Include(x => x.Status).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Reply(int id)
        {
            var msg = await _context.Messages.Where(y => y.Id == id).FirstOrDefaultAsync();
            msg.ReplyStatusId = 2;
            _context.Update(msg);
            await _context.SaveChangesAsync();
            return View(msg);
        }
        [HttpPost]
        public async Task<IActionResult> MessageReply(string reply, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var msgCount = await _context.MessageReplies.Where(x => x.MsgId == id).CountAsync();
                    if (msgCount > 0)
                    {
                        return Json("Already Replied!");
                    }

                    MessageReply replyMsg = new MessageReply()
                    {

                        RepliedMsg = reply,
                        CreatedAt = DateTime.Now,
                        MsgId = id
                    };
                    _context.Add(replyMsg);
                    await _context.SaveChangesAsync();

                    //===change status of the message
                    //===set to replied
                    var message = await _context.Messages.FindAsync(id);
                    message.ReplyStatusId = 3;
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                    return Json("The Reply has been sent successfully.");
                }
                return Json("Failed to send.");

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }


        }

        public async Task<IActionResult> MessageWithReply(int id)
        {
            MessageReply msgWithReply = await _context.MessageReplies.Include(x=>x.Message).Where(x => x.MsgId == id).FirstOrDefaultAsync();
            
            if (msgWithReply != null)
            {
                msgWithReply.Id = 0;
                msgWithReply.Message.Id = 0;
                msgWithReply.MsgId = 0;
                return Json(msgWithReply);
            }
                
            return Json(new MessageReply() { });

        }
    }
}
