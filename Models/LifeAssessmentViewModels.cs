using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    // Main ViewModel for the Life Assessment Index page
    public class LifeAssessmentIndexViewModel
    {
        public List<AreaOfLifeFilterItem> LifeAreas { get; set; } = new();
        public List<QuestionItemViewModel> Questions { get; set; } = new();
        public int? SelectedAreaId { get; set; }
        public string SelectedAreaName { get; set; } = string.Empty;
    }

    // ViewModel for Life Area filter dropdown items
    public class AreaOfLifeFilterItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int QuestionCount { get; set; }
    }

    // ViewModel for displaying questions in the assessment
    public class QuestionItemViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int? Answer { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        public bool IsRequired { get; set; } = true;
    }

    // ViewModel for submitting question answers
    public class QuestionAnswerViewModel
    {
        [Required]
        public int QuestionId { get; set; }
        
        [Required]
        [Range(1, 10, ErrorMessage = "Answer must be between 1 and 10")]
        public int AnswerValue { get; set; }
        
        public int AreaId { get; set; }
    }

    // ViewModel for the response after submitting answers
    public class SubmitAnswersResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        public double AverageScore { get; set; }
        public string AreaName { get; set; } = string.Empty;
    }

    // ViewModel for assessment statistics (optional future use)
    public class AssessmentStatsViewModel
    {
        public int TotalAssessments { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalAreas { get; set; }
        public double OverallAverageScore { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<AreaScoreViewModel> AreaScores { get; set; } = new();
    }

    // ViewModel for area-specific scores
    public class AreaScoreViewModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public double AverageScore { get; set; }
        public int QuestionCount { get; set; }
        public int ResponseCount { get; set; }
        public string Icon { get; set; } = string.Empty;
    }

    // ViewModel for AJAX responses when loading questions
    public class GetQuestionsResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<QuestionItemViewModel> Questions { get; set; } = new();
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
    }
}