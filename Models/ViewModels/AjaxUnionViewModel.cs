using EcdsApp.Models.RegionModels;
using System.Collections.Generic;

namespace EcdsApp.Models.ViewModels
{
    public class AjaxUnionViewModel
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IList<AdminBoundaryUnion> Data { get; set; }
    }
}
