using System;

namespace EcdsApp.Models.ViewModels.HitCountLogInfo
{
    public class WeekMonthDateTimeVM
    {
        public DateTime FirstDayOfLastMonth { get; set; }
        public DateTime LastDayOfLastMonth { get; set; }
        public DateTime FirstDayOfLastWeek { get; set; }
        public DateTime LastDayOfLastWeek { get; set; }
        public DateTime Yesterday { get; set; }
        
    }
}