using System;
using System.Collections.Generic;

namespace ToolBox.Models.ViewModels
{
    // ViewModel principal para ver respuestas del coach
    public class ViewResponsesViewModel
    {
        public long InstanceId { get; set; }
        public string QuestionnaireTitle { get; set; } = string.Empty;
        public string? QuestionnaireDescription { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string? ClientAvatarUrl { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public QuestionnaireStatus Status { get; set; }
        public List<QuestionAnswerViewModel> QuestionAnswers { get; set; } = new List<QuestionAnswerViewModel>();
        
        public TimeSpan? CompletionTime 
        {
            get
            {
                if (StartedAt.HasValue && CompletedAt.HasValue)
                {
                    return CompletedAt.Value - StartedAt.Value;
                }
                return null;
            }
        }
    }

    // ViewModel para cada pregunta y su respuesta
    public class QuestionAnswerViewModel
    {
        public long QuestionTemplateId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public int QuestionOrder { get; set; }
        
        // Opciones originales de la pregunta (para contexto)
        public List<string> OriginalOptions { get; set; } = new List<string>();
        public List<LikertOptionViewModel> OriginalLikertOptions { get; set; } = new List<LikertOptionViewModel>();
        
        // Respuesta del cliente
        public string? ClientResponseText { get; set; }
        public List<string> ClientMultipleChoiceResponse { get; set; } = new List<string>();
        public int? ClientLikertResponse { get; set; }
        public DateTime? AnsweredAt { get; set; }
        
        // Propiedades computadas para facilitar la vista
        public bool HasResponse => !string.IsNullOrWhiteSpace(ClientResponseText);
        public string FormattedResponse
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ClientResponseText))
                    return "Sin respuesta";
                    
                return QuestionType switch
                {
                    QuestionType.MultipleChoice => string.Join(", ", ClientMultipleChoiceResponse),
                    QuestionType.LikertScale => GetLikertResponseFormatted(),
                    _ => ClientResponseText
                };
            }
        }
        
        private string GetLikertResponseFormatted()
        {
            if (!ClientLikertResponse.HasValue) return "Sin respuesta";
            
            var likertValue = ClientLikertResponse.Value;
            var matchingOption = OriginalLikertOptions.FirstOrDefault(o => o.Value == likertValue);
            return matchingOption != null 
                ? $"{likertValue} - {matchingOption.Text}"
                : likertValue.ToString();
        }
    }

    // ViewModel para lista mejorada de asignaciones del coach
    public class CoachAssignmentsIndexViewModel
    {
        public List<CoachAssignmentItemViewModel> Assignments { get; set; } = new List<CoachAssignmentItemViewModel>();
        public List<QuestionnaireTemplateSelectViewModel> AvailableTemplates { get; set; } = new List<QuestionnaireTemplateSelectViewModel>();
        public List<ClientSelectViewModel> AvailableClients { get; set; } = new List<ClientSelectViewModel>();
        
        // Filtros actuales
        public long? SelectedTemplateId { get; set; }
        public int? SelectedClientId { get; set; }
        public string? SelectedStatus { get; set; }
        public string? SearchQuery { get; set; }
        
        // Paginación
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

    public class CoachAssignmentItemViewModel
    {
        public long InstanceId { get; set; }
        public string QuestionnaireTitle { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string? ClientAvatarUrl { get; set; }
        public DateTime AssignedAt { get; set; }
        public QuestionnaireStatus Status { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        
        public int ProgressPercentage => TotalQuestions > 0 
            ? (int)Math.Round((double)AnsweredQuestions / TotalQuestions * 100) 
            : 0;
    }

    public class QuestionnaireTemplateSelectViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AssignmentCount { get; set; }
    }

    public class ClientSelectViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AssignmentCount { get; set; }
    }
}