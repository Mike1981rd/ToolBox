using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El texto de la pregunta es requerido")]
        [StringLength(500, ErrorMessage = "La pregunta no puede exceder los 500 caracteres")]
        [Display(Name = "Pregunta")]
        public string QuestionText { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Debe seleccionar un área de vida")]
        [Display(Name = "Área de Vida")]
        public int LifeAreaId { get; set; }
        
        // Para mostrar en listados
        public string? LifeAreaTitle { get; set; }
        public string? LifeAreaIconClass { get; set; }
        public string? LifeAreaIconColor { get; set; }
    }
    
    public class QuestionCreateViewModel
    {
        [Required(ErrorMessage = "El texto de la pregunta es requerido")]
        [StringLength(500, ErrorMessage = "La pregunta no puede exceder los 500 caracteres")]
        [Display(Name = "Pregunta")]
        public string QuestionText { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Debe seleccionar un área de vida")]
        [Display(Name = "Área de Vida")]
        public int LifeAreaId { get; set; }
    }
    
    public class QuestionEditViewModel : QuestionCreateViewModel
    {
        public int Id { get; set; }
    }
}