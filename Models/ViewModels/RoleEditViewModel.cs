using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class RoleEditViewModel
    {
        public int Id { get; set; } // ID del rol que se está editando

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del rol no puede exceder los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        // Descripción sigue siendo opcional
        [StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un dashboard asignado.")]
        public string AssignedDashboard { get; set; } = string.Empty; // Ej: "Admin", "Coach", "Client"

        public bool IsActive { get; set; } // Permitir activar/desactivar el rol

        // Dashboard Permission Properties
        public bool PuedeVerMensajeBienvenidaDashboard { get; set; }
        public bool PuedeVerVideoBienvenidaDashboard { get; set; }
        public bool PuedeVerCardTotalClientesDashboard { get; set; }
        public bool PuedeVerCardClientesActivosDashboard { get; set; }

        // Para mostrar todos los permisos disponibles y marcar los ya seleccionados
        public List<PermissionViewModel> AvailablePermissions { get; set; } // Reutilizamos PermissionViewModel

        // Para capturar los IDs de los permisos seleccionados desde el formulario
        [Required(ErrorMessage = "Debe seleccionar al menos un permiso.")] // Considera si un rol puede no tener permisos
        public List<int> SelectedPermissionIds { get; set; }

        public RoleEditViewModel()
        {
            AvailablePermissions = new List<PermissionViewModel>();
            SelectedPermissionIds = new List<int>();
        }
    }
    // Nota: El PermissionViewModel definido anteriormente (con Id, ModuleName, ActionName, Description, Category, IsSelected)
    // se reutilizará. La propiedad 'IsSelected' será clave aquí.
}