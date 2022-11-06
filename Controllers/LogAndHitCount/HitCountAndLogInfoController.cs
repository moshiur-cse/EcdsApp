using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EcdsApp.Data;
using EcdsApp.Models.ViewModels.HitCountLogInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EcdsApp.Controllers.LogAndHitCount
{
    public class HitCountAndLogInfoController : Controller
    {
        private readonly DataContext _context;

        public HitCountAndLogInfoController(DataContext context)
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
            weekMonthDateTime.FirstDayOfLastWeek = DateTime.Today.AddDays(-8-today.Day);
            weekMonthDateTime.LastDayOfLastWeek = DateTime.Today.AddDays(-2-today.Day);
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
        
        public async Task<IActionResult> LogDetails()
        {
            var data = _context.UserLogs.Include(x=>x.LogType).ToListAsync();
            return View(data);
        }
    }
}