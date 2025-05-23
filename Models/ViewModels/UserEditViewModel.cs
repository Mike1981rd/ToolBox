using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToolBox.Models.ViewModels
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder los 200 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "El nombre de usuario solo puede contener letras, números, guiones bajos, puntos y guiones.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
        [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres.")]
        public string Email { get; set; } = string.Empty;

        // Contraseña es opcional en la edición
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres.")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Las nuevas contraseñas no coinciden.")]
        public string? ConfirmNewPassword { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder los 20 caracteres.")]
        public string? PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "El nombre de la empresa no puede exceder los 200 caracteres.")]
        public string? CompanyName { get; set; }

        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string? Country { get; set; }

        [StringLength(50, ErrorMessage = "El ID fiscal no puede exceder los 50 caracteres.")]
        public string? TaxId { get; set; }
        
        [Display(Name = "Rol")]
        public int? RoleId { get; set; }

        [StringLength(100, ErrorMessage = "El método de facturación no puede exceder los 100 caracteres.")]
        public string? BillingMethod { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(10, ErrorMessage = "El idioma no puede exceder los 10 caracteres.")]
        public string? Language { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        // Para mostrar el avatar actual
        public string? ExistingAvatarUrl { get; set; }

        // Para subir un nuevo avatar
        [Display(Name = "Nuevo Avatar")]
        public IFormFile? AvatarFile { get; set; }

        // Para poblar el dropdown de Roles
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }

        public UserEditViewModel()
        {
            AvailableRoles = new List<SelectListItem>();
        }
    }
}