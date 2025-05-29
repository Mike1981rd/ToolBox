using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    // ViewModel para crear/editar plantilla de cuestionario
    public class QuestionnaireTemplateCreateEditViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El título es requerido")]
        [StringLength(255, ErrorMessage = "El título no puede exceder 255 caracteres")]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "La descripción no puede exceder 2000 caracteres")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Para la vista de edición - cargar preguntas existentes
        public List<QuestionTemplateViewModel> Questions { get; set; } = new List<QuestionTemplateViewModel>();
    }

    // ViewModel para listar plantillas
    public class QuestionnaireTemplateListViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int QuestionCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CoachName { get; set; } = string.Empty;
        
        // Para descripción truncada en la lista
        public string ShortDescription => 
            string.IsNullOrEmpty(Description) 
                ? "" 
                : Description.Length > 100 
                    ? Description.Substring(0, 100) + "..." 
                    : Description;
    }

    // ViewModel para respuestas de paginación
    public class QuestionnaireTemplatePaginatedResponseViewModel
    {
        public List<QuestionnaireTemplateListViewModel> Templates { get; set; } = new();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

    // ViewModel para gestión de preguntas individuales
    public class QuestionTemplateViewModel
    {
        public long Id { get; set; }
        public long QuestionnaireTemplateId { get; set; }

        [Required(ErrorMessage = "El texto de la pregunta es requerido")]
        [StringLength(1000, ErrorMessage = "El texto de la pregunta no puede exceder 1000 caracteres")]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        // Para opciones de respuesta (SingleChoice, MultipleChoice, LikertScale)
        public List<string> Options { get; set; } = new List<string>();

        // Para escala Likert - opciones más estructuradas
        public List<LikertOptionViewModel> LikertOptions { get; set; } = new List<LikertOptionViewModel>();

        public bool IsRequired { get; set; } = true;
        public int Order { get; set; }

        // Para vista
        public string TypeDisplayName => Type switch
        {
            QuestionType.SingleChoice => "Opción Múltiple (Una respuesta)",
            QuestionType.MultipleChoice => "Opción Múltiple (Múltiples respuestas)",
            QuestionType.ShortText => "Respuesta Corta",
            QuestionType.LongText => "Respuesta Larga",
            QuestionType.LikertScale => "Escala Likert",
            QuestionType.TrueFalse => "Verdadero/Falso",
            _ => Type.ToString()
        };
    }

    // ViewModel para opciones de escala Likert
    public class LikertOptionViewModel
    {
        public int Value { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    // ViewModel para crear/editar pregunta (para modales/AJAX)
    public class QuestionTemplateCreateEditViewModel
    {
        public long Id { get; set; }
        public long QuestionnaireTemplateId { get; set; }

        [Required(ErrorMessage = "El texto de la pregunta es requerido")]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public QuestionType Type { get; set; }

        // Opciones como string JSON para simplificar el manejo en JavaScript
        public string? OptionsJson { get; set; }

        public bool IsRequired { get; set; } = true;
        public int Order { get; set; }
    }

    // ViewModel para reordenar preguntas
    public class ReorderQuestionsViewModel
    {
        public List<QuestionOrderViewModel> Questions { get; set; } = new List<QuestionOrderViewModel>();
    }

    public class QuestionOrderViewModel
    {
        public long Id { get; set; }
        public int Order { get; set; }
    }
}