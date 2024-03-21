using EcdsApp.Data;
using EcdsApp.Models.SystemCommon;
using EcdsApp.Models.UserManage;
using EcdsApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System;
using System.IO;
using GeoAPI.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        [Authorize]
        public async Task<IActionResult> DefaultUserMessages()
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var msgSenders = _context.Chats.Where(x => x.Receiver == loggedInUser.Email).Select(x => x.Sender).ToList();
            List<ChatUserInfoViewModel> appUsers = new() { };
            foreach (var sender in msgSenders)
            {
                var user = await _userManager.FindByEmailAsync(sender);
                ChatUserInfoViewModel chatUser = new()
                {
                    Name = user.FullName,
                    Email = user.Email,
                    ImageUrl = user.ProfilePic!=null? "/assets/profile_pics" +user.ProfilePic: "/assets/profile_pics/user_icon.png",
                    UrlToGetIndvMsg = Url.Action("DisplayChats").ToString(),
                };
                if(!appUsers.Where(x=>x.Email==user.Email).Any())
                    appUsers.Add(chatUser);
            }
            return Json(appUsers);
        }

        [Authorize]
        public async Task<IActionResult> DisplayChats(string sender)
        {
            var user = await _userManager.GetUserAsync(User);
            var receiver = user.Email;
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

        //====built for testing functionality

        //public IActionResult DisplayChats(string receiver, string sender)
        //{
        //    if (!(string.IsNullOrEmpty(receiver) || string.IsNullOrEmpty(receiver)))
        //    {
        //        var chats = _context.Chats
        //            .Where(x => x.Sender == sender || x.Sender == receiver)
        //            .Where(y => y.Receiver == receiver || y.Receiver == sender)
        //            .OrderBy(y => y.SentAt)
        //            .Select(x => new { x.Sender, x.Receiver, x.Message, x.SentAt })
        //            .ToList();
        //        if (chats == null)
        //            return Json("not found");
        //        return Json(chats);
        //    }
        //    return Json("error");
        //}

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

        //public IActionResult PersonalChat(string email)
        //{
        //    // Replace "path/to/your/shapefile.shp" with the actual path to your shapefile
        //    string shapefilePath = "path/to/your/shapefile.shp";

        //    // Replace "path/to/your/output.geojson" with the desired output GeoJSON file path
        //    string geojsonPath = "path/to/your/output.geojson";

        //    try
        //    {
        //        var features = ReadShapefile(shapefilePath);
        //        WriteGeoJson(features, geojsonPath);

        //        Console.WriteLine("Conversion successful.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}
    }
}
