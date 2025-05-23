using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del rol no puede exceder los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un dashboard asignado.")]
        public string AssignedDashboard { get; set; } = string.Empty;

        public List<PermissionViewModel> AvailablePermissions { get; set; }

        public List<int>? SelectedPermissionIds { get; set; }

        public RoleCreateViewModel()
        {
            AvailablePermissions = new List<PermissionViewModel>();
            SelectedPermissionIds = new List<int>();
        }
    }

    public class PermissionViewModel
    {
        public int Id { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool IsSelected { get; set; }
    }
}