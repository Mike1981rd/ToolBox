using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class UserAnswerViewModel
    {
        public int QuestionId { get; set; }
        
        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
        public int Score { get; set; }
    }
    
    public class SaveAnswersRequestViewModel
    {
        [Required]
        public int AreaId { get; set; }
        
        // No longer required - allows partial saves
        public List<UserAnswerViewModel>? Answers { get; set; } = new List<UserAnswerViewModel>();
    }
    
    public class QuestionWithAnswerViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int? CurrentScore { get; set; } // null if user hasn't answered yet
        public int LifeAreaId { get; set; }
        public string LifeAreaTitle { get; set; } = string.Empty;
    }
    
    public class AreaQuestionsResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public List<QuestionWithAnswerViewModel> Questions { get; set; } = new List<QuestionWithAnswerViewModel>();
        public int TotalQuestions { get; set; }
        public int AnsweredQuestions { get; set; }
        public decimal CompletionPercentage { get; set; }
    }
    
    public class SaveAnswersResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public int SavedCount { get; set; }
        public int UpdatedCount { get; set; }
    }
}