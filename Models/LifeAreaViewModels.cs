using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class LifeAreaListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string IconClass { get; set; } = string.Empty;
        public string IconColor { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class LifeAreaCreateViewModel
    {
        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(100, ErrorMessage = "El título no puede exceder 100 caracteres")]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un icono")]
        [Display(Name = "Icono")]
        public string IconClass { get; set; } = "fas fa-circle";

        [Required(ErrorMessage = "Debe seleccionar un color")]
        [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "El color debe ser un código hexadecimal válido")]
        [Display(Name = "Color del Icono")]
        public string IconColor { get; set; } = "#6c757d";

        [Display(Name = "Estado")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Orden de Visualización")]
        public int DisplayOrder { get; set; } = 0;
    }

    public class LifeAreaEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(100, ErrorMessage = "El título no puede exceder 100 caracteres")]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un icono")]
        [Display(Name = "Icono")]
        public string IconClass { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un color")]
        [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "El color debe ser un código hexadecimal válido")]
        [Display(Name = "Color del Icono")]
        public string IconColor { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        public bool IsActive { get; set; }

        [Display(Name = "Orden de Visualización")]
        public int DisplayOrder { get; set; }
    }

    // ViewModel para el selector de iconos
    public class IconOption
    {
        public string Class { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}