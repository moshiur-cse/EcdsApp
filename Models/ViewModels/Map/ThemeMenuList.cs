using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.ViewModels.Map
{
    public class SubThemeList
    {
        public string subThemeName { get; set; }
        public List<string> layerPathList { get; set; }
        public List<int> layerIdList { get; set; }
        public List<string> layerNameList { get; set; }
        public List<int> layerTypeIdList { get; set; }
        public List<int?> tableIdList { get; set; }
    }

    public class ThemeList
    {
        public string themeName { get; set; }
        public List<SubThemeList> subThemeList { get; set; }
    }
}
