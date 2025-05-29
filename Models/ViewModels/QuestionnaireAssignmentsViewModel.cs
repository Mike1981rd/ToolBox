using System;
using System.Collections.Generic;

namespace ToolBox.Models.ViewModels
{
    public class QuestionnaireAssignmentsViewModel
    {
        public long TemplateId { get; set; }
        public string TemplateTitle { get; set; } = string.Empty;
        public string? TemplateDescription { get; set; }
        public List<QuestionnaireAssignmentItemViewModel> Assignments { get; set; } = new List<QuestionnaireAssignmentItemViewModel>();
    }

    public class QuestionnaireAssignmentItemViewModel
    {
        public long InstanceId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public string? ClientAvatarUrl { get; set; }
        public QuestionnaireStatus Status { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
    }
}