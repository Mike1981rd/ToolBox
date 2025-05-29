using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    // ViewModel para la lista de cuestionarios del cliente
    public class ClientQuizInstanceViewModel
    {
        public long InstanceId { get; set; }
        public string QuestionnaireTitle { get; set; } = string.Empty;
        public string? QuestionnaireDescription { get; set; }
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

    // ViewModel para responder un cuestionario
    public class RespondQuizViewModel
    {
        public long InstanceId { get; set; }
        public string QuestionnaireTitle { get; set; } = string.Empty;
        public string? QuestionnaireDescription { get; set; }
        public QuestionnaireStatus Status { get; set; }
        public List<QuestionToAnswerViewModel> Questions { get; set; } = new List<QuestionToAnswerViewModel>();
    }

    // ViewModel para cada pregunta a responder
    public class QuestionToAnswerViewModel
    {
        public long QuestionTemplateId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        
        // Opciones para preguntas de opción múltiple o escala Likert
        public List<string> Options { get; set; } = new List<string>();
        public List<LikertOptionViewModel> LikertOptions { get; set; } = new List<LikertOptionViewModel>();
        
        // Respuestas del cliente (para poblar formulario si ya respondió)
        public string? ClientResponseText { get; set; }
        public List<string> ClientMultipleChoiceResponse { get; set; } = new List<string>();
        public int? ClientLikertResponse { get; set; }
    }

    // ViewModel para envío de respuestas
    public class SaveQuizResponsesViewModel
    {
        public long InstanceId { get; set; }
        public List<ClientAnswerViewModel> Answers { get; set; } = new List<ClientAnswerViewModel>();
    }

    public class ClientAnswerViewModel
    {
        public long QuestionTemplateId { get; set; }
        public string? ResponseText { get; set; }
    }

    // LikertOptionViewModel se encuentra en QuestionnaireTemplateViewModels.cs
}