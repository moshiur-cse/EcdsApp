using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.ViewModels.Dashboard
{
    public class ChartDataVm
    {
        public string name { get; set; }

        //[Column(TypeName = "decimal(12, 2)")]
        public dynamic children { get; set; }
        public string description { get; set; }
    }
}
