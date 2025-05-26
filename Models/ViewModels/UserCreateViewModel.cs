using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Para IFormFile
using Microsoft.AspNetCore.Mvc.Rendering; // Para SelectList

namespace ToolBox.Models.ViewModels
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "El nombre de usuario solo puede contener letras, números, guiones bajos, puntos y guiones.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? TaxId { get; set; }
        
        [Display(Name = "Rol")]
        public int? RoleId { get; set; } // Para seleccionar el Rol

        public string? BillingMethod { get; set; }
        public bool IsActive { get; set; } = true; // Default activo
        public string? Language { get; set; }
        public string DefaultLanguage { get; set; } = "es";
        public bool IsTwoFactorEnabled { get; set; }

        // Para poblar el dropdown de Roles
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }

        // Para el avatar
        [Display(Name = "Avatar")]
        public IFormFile? AvatarFile { get; set; }

        public UserCreateViewModel()
        {
            AvailableRoles = new List<SelectListItem>();
        }
    }
}