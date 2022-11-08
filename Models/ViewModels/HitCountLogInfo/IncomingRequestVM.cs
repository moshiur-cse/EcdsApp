using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace EcdsApp.Models.ViewModels.HitCountLogInfo
{
    public class IncomingRequestVM
    {
        public int TotalHitRequest { get; set; }
        public int TotalHitRequestUnique { get; set; }
        public int TotalHitRequestLastWeek { get; set; }
        public int TotalHitRequestLastMonth { get; set; }
        public List<YearWiseCount> YearWiseCounts {get;set;}
        
        public string YearWiseCountsInString {get;set;}
    }

    public class YearWiseCount
    {
        public int Year { get; set; }
        public int TotalSentRequests { get; set; }
    }
}