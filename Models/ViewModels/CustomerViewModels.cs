using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class CustomerListItemViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string CustomerNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; }
        public string? StatusDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserName { get; set; }
    }

    public class CustomerCreateViewModel
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder los 20 caracteres.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "El nombre de la empresa no puede exceder los 200 caracteres.")]
        public string? CompanyName { get; set; }

        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string? Country { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(100, ErrorMessage = "El detalle del estado no puede exceder los 100 caracteres.")]
        public string? StatusDetail { get; set; }

        // Para la subida de avatar
        public IFormFile? AvatarFile { get; set; }
        public string? CurrentAvatarUrl { get; set; }
    }

    public class CustomerEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(200, ErrorMessage = "El email no puede exceder los 200 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder los 20 caracteres.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "El nombre de la empresa no puede exceder los 200 caracteres.")]
        public string? CompanyName { get; set; }

        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string? Country { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(100, ErrorMessage = "El detalle del estado no puede exceder los 100 caracteres.")]
        public string? StatusDetail { get; set; }

        // Para la subida de avatar
        public IFormFile? AvatarFile { get; set; }
        public string? CurrentAvatarUrl { get; set; }

        // Información adicional para la vista
        public string CustomerNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? CreatedByUserName { get; set; }
    }

    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string CustomerNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; }
        public string? StatusDetail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedByUserName { get; set; }
    }
}