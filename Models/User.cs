using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder los 200 caracteres.")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255, ErrorMessage = "El hash de la contraseña no puede exceder los 255 caracteres.")]
        public string PasswordHash { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder los 20 caracteres.")]
        public string? PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "El nombre de la empresa no puede exceder los 200 caracteres.")]
        public string? CompanyName { get; set; }

        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string? Country { get; set; }

        [StringLength(50, ErrorMessage = "El ID fiscal no puede exceder los 50 caracteres.")]
        public string? TaxId { get; set; }

        [StringLength(100, ErrorMessage = "El método de facturación no puede exceder los 100 caracteres.")]
        public string? BillingMethod { get; set; }

        [StringLength(10, ErrorMessage = "El idioma no puede exceder los 10 caracteres.")]
        public string? Language { get; set; }

        [StringLength(5, ErrorMessage = "El idioma predeterminado no puede exceder los 5 caracteres.")]
        public string DefaultLanguage { get; set; } = "es";

        [Url(ErrorMessage = "El formato de la URL del avatar no es válido.")]
        [StringLength(500, ErrorMessage = "La URL del avatar no puede exceder los 500 caracteres.")]
        public string? AvatarUrl { get; set; }

        public bool IsTwoFactorEnabled { get; set; } = false;

        public bool IsActive { get; set; } = true;

        [StringLength(100, ErrorMessage = "El detalle del estado no puede exceder los 100 caracteres.")]
        public string? StatusDetail { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relación con Rol
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}