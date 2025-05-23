namespace ToolBox.Models.ViewModels
{
    public class RoleListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedDashboard { get; set; }
        public bool IsActive { get; set; }
        public int UserCount { get; set; }
    }
}