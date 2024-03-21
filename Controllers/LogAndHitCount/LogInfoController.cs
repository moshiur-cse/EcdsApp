using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcdsApp.Data;
using EcdsApp.Models.ViewModels.HitCountLogInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EcdsApp.Controllers.LogAndHitCount
{
    public class LogInfoController : Controller
    {
        private readonly DataContext _context;

        public LogInfoController(DataContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> IncomingRequests()
        {
            var hitInfos = await _context.ServerHitInfos.ToListAsync();
            var weekMonthDateTime = new WeekMonthDateTimeVM();
            var today = DateTime.Today;
            var monthStart = new DateTime(today.Year, today.Month, 1);
            weekMonthDateTime.FirstDayOfLastMonth = monthStart.AddMonths(-1);
            weekMonthDateTime.LastDayOfLastMonth = monthStart.AddDays(-1);
            weekMonthDateTime.FirstDayOfLastWeek = today.AddDays( -(int)DateTime.Now.DayOfWeek - 7 );
            weekMonthDateTime.LastDayOfLastWeek = weekMonthDateTime.FirstDayOfLastWeek.AddDays(6);
            //weekMonthDateTime.Yesterday = DateTime.Today.AddDays(-1);
            
            var groupbyList = hitInfos.GroupBy(x => x.RequestedAt.Date.Year).ToList();
            List<YearWiseCount> yearCountList = new();
            foreach (var item in groupbyList)
            {
                yearCountList.Add(new()
                {
                    Year = item.Key,
                    TotalSentRequests = item.Count()                    
                });                
            }
                var incomingReq = new IncomingRequestVM();
                incomingReq.TotalHitRequestLastMonth=hitInfos.Where(x =>
                    x.RequestedAt >= weekMonthDateTime.FirstDayOfLastMonth &&
                    x.RequestedAt <= weekMonthDateTime.LastDayOfLastMonth).Count();
                incomingReq.TotalHitRequestLastWeek=hitInfos.Where(x =>
                    x.RequestedAt >= weekMonthDateTime.FirstDayOfLastWeek &&
                    x.RequestedAt <= weekMonthDateTime.LastDayOfLastWeek).Count();
                incomingReq.TotalHitRequestUnique=hitInfos.GroupBy(x =>
                    x.IPAddress).Count();
                incomingReq.TotalHitRequest = hitInfos.Count;
                incomingReq.YearWiseCountsInString = JsonConvert.SerializeObject(yearCountList);
                return View(incomingReq);


        }
        
        // public async Task<IActionResult> IncomingRequestsJson()
        // {
        //     var hitInfos = await _context.ServerHitInfos.ToListAsync();
        //     var weekMonthDateTime = new WeekMonthDateTimeVM();
        //     var today = DateTime.Today;
        //     var monthStart = new DateTime(today.Year, today.Month, 1);
        //     weekMonthDateTime.FirstDayOfLastMonth = monthStart.AddMonths(-1);
        //     weekMonthDateTime.LastDayOfLastMonth = monthStart.AddDays(-1);
        //     //weekMonthDateTime.FirstDayOfLastWeek = DateTime.Today.AddDays(today.Day-7);
        //     weekMonthDateTime.FirstDayOfLastWeek = today.AddDays( -(int)DateTime.Now.DayOfWeek - 7 );
        //     weekMonthDateTime.LastDayOfLastWeek = weekMonthDateTime.FirstDayOfLastWeek.AddDays(6);
        //     //weekMonthDateTime.Yesterday = DateTime.Today.AddDays(-1);
        //     
        //     var groupbyList = hitInfos.GroupBy(x => x.RequestedAt.Date.Year).ToList();
        //     List<YearWiseCount> yearCountList = new();
        //     foreach (var item in groupbyList)
        //     {
        //         yearCountList.Add(new()
        //         {
        //             Year = item.Key,
        //             TotalSentRequests = item.Count()                    
        //         });                
        //     }
        //     var incomingReq = new IncomingRequestVM();
        //     incomingReq.TotalHitRequestLastMonth=hitInfos.Where(x =>
        //         x.RequestedAt >= weekMonthDateTime.FirstDayOfLastMonth &&
        //         x.RequestedAt <= weekMonthDateTime.LastDayOfLastMonth).Count();
        //     incomingReq.TotalHitRequestLastWeek=hitInfos.Where(x =>
        //         x.RequestedAt >= weekMonthDateTime.FirstDayOfLastWeek &&
        //         x.RequestedAt <= weekMonthDateTime.LastDayOfLastWeek).Count();
        //     incomingReq.TotalHitRequestUnique=hitInfos.GroupBy(x =>
        //         x.IPAddress).Count();
        //     incomingReq.TotalHitRequest = hitInfos.Count;
        //     incomingReq.YearWiseCountsInString = JsonConvert.SerializeObject(yearCountList);
        //     return Json(incomingReq);
        // }
        
        public async Task<IActionResult> LogDetails()
        {
            var data =  await _context.DownloadLogs.Include(x=>x.ThemeLayerDetail).Include(x=>x.User).ToListAsync();
            return View(data);
        }
    }
}