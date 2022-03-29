using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcdsApp.Models.ViewModels.UserManage
{
    public class UserAccessVm
    {
        public Dictionary<string, List<MenuContent>> Data { get; set; }

        public string RoleName { get; set; }
        public string[] ComponentArray { get; set; }
        public string[] ContentArray { get; set; }
        public string ActionMode { get; set; }
    }

    public class MenuContent
    {
        public int ContentId { get; set; }
        public string Action { get; set; }
        public bool IsChecked { get; set; }
    }
}
