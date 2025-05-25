using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolBox.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder los 20 caracteres.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "El nombre de la empresa no puede exceder los 200 caracteres.")]
        public string? CompanyName { get; set; }

        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string? Country { get; set; }

        [Url(ErrorMessage = "El formato de la URL del avatar no es válido.")]
        [StringLength(500, ErrorMessage = "La URL del avatar no puede exceder los 500 caracteres.")]
        public string? AvatarUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(100, ErrorMessage = "El detalle del estado no puede exceder los 100 caracteres.")]
        public string? StatusDetail { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relación con el usuario que creó el cliente (para auditoría)
        public int? CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User? CreatedByUser { get; set; }

        // Propiedades calculadas para compatibilidad con vistas existentes
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}".Trim();

        [NotMapped]
        public string CustomerNumber => $"CL{Id:D6}"; // Formato: CL000001, CL000002, etc.
    }
}