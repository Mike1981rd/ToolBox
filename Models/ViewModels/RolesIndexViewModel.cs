namespace ToolBox.Models.ViewModels
{
    public class RolesIndexViewModel
    {
        public IEnumerable<RoleListItemViewModel> Roles { get; set; } = new List<RoleListItemViewModel>();
        public IEnumerable<UserListItemViewModel> AllUsers { get; set; } = new List<UserListItemViewModel>();
    }
}