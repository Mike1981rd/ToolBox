using System.Collections.Generic;

namespace ToolBox.Models.ViewModels
{
    public class RolesIndexPageViewModel
    {
        public IEnumerable<RoleListItemViewModel> RoleCards { get; set; }
        public IEnumerable<UserListItemViewModel> AllSystemUsers { get; set; }
    }
} 