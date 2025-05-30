using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToolBox.Models.ViewModels
{
    public class Feedback360ResponseViewModel
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        
        public string SubjectUserName { get; set; } = string.Empty;
        public string InstanceTitle { get; set; } = string.Empty;
        public RaterCategory CurrentRaterCategory { get; set; }
        public DateTime? FeedbackDeadline { get; set; }
        
        public List<CompetencyResponseViewModel> Competencies { get; set; } = new List<CompetencyResponseViewModel>();
        public List<OpenEndedQuestionResponseViewModel> OpenEndedQuestions { get; set; } = new List<OpenEndedQuestionResponseViewModel>();
        
        // Helper properties
        public bool IsSelfEvaluation => CurrentRaterCategory == RaterCategory.Self;
        public bool IsDeadlinePassed => FeedbackDeadline.HasValue && FeedbackDeadline.Value < DateTime.UtcNow;
    }

    public class CompetencyResponseViewModel
    {
        public string CompetencyCode { get; set; } = string.Empty;
        public string CompetencyName { get; set; } = string.Empty;
        public List<ScaleQuestionResponseViewModel> ScaleQuestions { get; set; } = new List<ScaleQuestionResponseViewModel>();
    }

    public class ScaleQuestionResponseViewModel
    {
        public string QuestionCode { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        
        [Range(1, 5, ErrorMessage = "Score must be between 1 and 5.")]
        public int? Score { get; set; }
        
        public List<SelectListItem> LikertScaleOptions { get; set; } = new List<SelectListItem>();
        
        // For validation
        public bool IsRequired { get; set; } = true;
    }

    public class OpenEndedQuestionResponseViewModel
    {
        public string QuestionCode { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        
        [DataType(DataType.MultilineText)]
        public string ResponseText { get; set; } = string.Empty;
    }

    // Response submission models for AJAX
    public class SaveDraftResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime? LastSaved { get; set; }
    }

    // Models for thank you page
    public class Feedback360ThankYouViewModel
    {
        public string SubjectUserName { get; set; } = string.Empty;
        public string InstanceTitle { get; set; } = string.Empty;
        public bool WasSelfEvaluation { get; set; }
    }

    // Error view model
    public class Feedback360ErrorViewModel
    {
        public string ErrorType { get; set; } = string.Empty; // "InvalidToken", "AlreadyCompleted", "DeadlinePassed"
        public string Message { get; set; } = string.Empty;
        public DateTime? Deadline { get; set; }
        
        public string ErrorIcon
        {
            get
            {
                return ErrorType switch
                {
                    "InvalidToken" => "fa-exclamation-triangle",
                    "AlreadyCompleted" => "fa-check-circle",
                    "DeadlinePassed" => "fa-clock",
                    _ => "fa-exclamation-circle"
                };
            }
        }
        
        public string ErrorColor
        {
            get
            {
                return ErrorType switch
                {
                    "InvalidToken" => "text-danger",
                    "AlreadyCompleted" => "text-success",
                    "DeadlinePassed" => "text-warning",
                    _ => "text-danger"
                };
            }
        }
    }
}