using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class WheelOfLifeAreaScoreViewModel
    {
        public int LifeAreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
        public string IconColor { get; set; } = string.Empty;
        public int? CurrentScore { get; set; } // null if user hasn't scored yet
    }
    
    public class WheelOfLifeDataViewModel
    {
        public List<WheelOfLifeAreaScoreViewModel> Areas { get; set; } = new List<WheelOfLifeAreaScoreViewModel>();
        public int TotalScore { get; set; }
        public decimal AverageScore { get; set; }
        public int AreasCount { get; set; }
    }
    
    public class SaveWheelScoreViewModel
    {
        public int LifeAreaId { get; set; }
        
        [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10")]
        public int Score { get; set; }
    }
    
    public class SaveWheelScoresRequestViewModel
    {
        public List<SaveWheelScoreViewModel> Scores { get; set; } = new List<SaveWheelScoreViewModel>();
    }
}