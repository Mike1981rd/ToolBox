using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToolBox.Models.ViewModels
{
    // ViewModel para asignar cuestionario
    public class AssignQuestionnaireViewModel
    {
        public long QuestionnaireTemplateId { get; set; }
        public string TemplateTitle { get; set; } = string.Empty;
        public string? TemplateDescription { get; set; }
        public int QuestionCount { get; set; }

        [Required(ErrorMessage = "Debes seleccionar al menos un cliente")]
        public List<int> SelectedClientIds { get; set; } = new List<int>();
        
        // Para mostrar lista de clientes disponibles
        public List<SelectListItem> AvailableClients { get; set; } = new List<SelectListItem>();
        
        // Para mostrar clientes que ya tienen asignado este cuestionario
        public List<ExistingAssignmentViewModel> AlreadyAssignedClients { get; set; } = new List<ExistingAssignmentViewModel>();
    }
    
    // ViewModel para mostrar asignaciones existentes
    public class ExistingAssignmentViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public QuestionnaireStatus Status { get; set; }
        public DateTime AssignedAt { get; set; }
    }

    // ViewModel simple para lista de clientes
    public class ClientListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public bool HasPendingInstance { get; set; } // Si ya tiene una instancia pendiente de este cuestionario
    }

    // ViewModel para listar instancias asignadas
    public class QuestionnaireInstanceListViewModel
    {
        public long Id { get; set; }
        public string TemplateTitle { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public QuestionnaireStatus Status { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        
        // Para mostrar progreso
        public int ProgressPercentage => TotalQuestions > 0 
            ? (int)Math.Round((double)AnsweredQuestions / TotalQuestions * 100) 
            : 0;
            
        // Para mostrar estado en UI
        public string StatusDisplayName => Status switch
        {
            QuestionnaireStatus.Pending => "Pendiente",
            QuestionnaireStatus.InProgress => "En Progreso",
            QuestionnaireStatus.Completed => "Completado",
            _ => Status.ToString()
        };
        
        public string StatusBadgeClass => Status switch
        {
            QuestionnaireStatus.Pending => "bg-warning",
            QuestionnaireStatus.InProgress => "bg-info",
            QuestionnaireStatus.Completed => "bg-success",
            _ => "bg-secondary"
        };
    }

    // ViewModel para respuestas paginadas
    public class QuestionnaireInstancePaginatedResponseViewModel
    {
        public List<QuestionnaireInstanceListViewModel> Instances { get; set; } = new();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}