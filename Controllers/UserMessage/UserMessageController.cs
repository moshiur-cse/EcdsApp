using EcdsApp.Data;
using EcdsApp.Models;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.UserMessage;
using EcdsApp.Services;
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
            var data = await _context.Messages.Include(x => x.Status).OrderByDescending(x=>x.CreatedAt).ThenBy(x=>x.ReplyStatusId).ToListAsync();
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
        public async Task<IActionResult> MessageReply(string reply, int id, [FromServices] IEmailSender _emailSender)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var replyCount = await _context.MessageReplies.Where(x => x.MsgId == id).CountAsync();
                    if (replyCount > 0)
                    {
                        return Json("Already Replied!");
                    }

                    //====try to send email
                    var userMsg= await _context.Messages.Where(x => x.Id == id).FirstOrDefaultAsync();
                    bool state = await _emailSender.SendEmailAsync(new Models.ViewModels.EmailModel()
                    {
                        Subject = "Response to your message in ECDS platform",
                        To = userMsg.Email,
                        Msg = $"Dear {userMsg.FullName},<br/><br/>{reply}<br/><br/>Best Wishes<br/>System Administrator, ECDS"
                    });

                    if (state)
                    {
                        //===Message is not replied yet so save it in db

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

                        userMsg.ReplyStatusId = 3;
                        _context.Update(userMsg);
                        await _context.SaveChangesAsync();
                        return Json("The Reply has been sent successfully.");
                    }
                    
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
            MessageReply msgWithReply = new MessageReply(){};        
            msgWithReply = await _context.MessageReplies.Include(x=>x.Message).Where(x => x.MsgId == id).FirstOrDefaultAsync();
            
            if (msgWithReply != null)
            {
                //===update state in reply status in db
                var message = msgWithReply.Message;
                message.ReplyStatusId = 2;
                _context.Update(message);
                await _context.SaveChangesAsync();

                //====send info back to user
                msgWithReply.Id = 0;
                msgWithReply.Message.Id = 0;
                msgWithReply.MsgId = 0;                
                return Json(msgWithReply);

            }
            var userMessage = await _context.Messages.FindAsync(id);
            Message msg = new()
            {
                Msg = userMessage.Msg,
                FullName = userMessage.FullName,
                Email = userMessage.Email,
                ReplyStatusId = 1
            };
            

            if (userMessage != null)
            {
                msgWithReply = new MessageReply() { Id = 0, MsgId = 0, RepliedMsg = "",Message=msg };
                return Json(msgWithReply);
            }
            
                
            return Json(new MessageReply() { });

        }

    }
}
