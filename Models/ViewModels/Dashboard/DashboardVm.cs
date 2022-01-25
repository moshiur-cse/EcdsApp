using EcdsApp.Models.ThemeModels;
using EcdsApp.Models.UpazilaWiseInfoModels;
using System.Collections.Generic;

namespace EcdsApp.Models.ViewModels.Dashboard
{
    public class DashboardVm
    {
        public IEnumerable<UpazilaWiseExposureData> UpazilaWiseExposureData { get; set; }
        public IEnumerable<ThemeLayerDetail> ThemeLayerDetails { get; set; }
    }
}
