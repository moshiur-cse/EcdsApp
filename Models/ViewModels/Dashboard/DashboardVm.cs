using EcdsApp.Models.DistrictWiseInfoModels;
using EcdsApp.Models.ThemeModels;
using EcdsApp.Models.UpazilaWiseInfoModels;
using System.Collections.Generic;

namespace EcdsApp.Models.ViewModels.Dashboard
{
    public class DashboardVm
    {
        public IEnumerable<DistrictWisePopulation> DistrictWisePopulations { get; set; }
        public IEnumerable<ThemeLayerDetail> ThemeLayerDetails { get; set; }
        public IEnumerable<LayerLegendColor> LayerLegendColors { get; set; }
        public IEnumerable<DistrictWisePoverty> DistrictWisePoverties { get; set; }
    }
}
