using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    // Main ViewModel for the Wheel of Life page
    public class WheelOfLifePageViewModel
    {
        public List<LifeAreaScoreViewModel> LifeAreas { get; set; } = new();
        public int TotalScore { get; set; }
        public double AverageScore { get; set; }
        public int MaxPossibleScore { get; set; }
        public string LastUpdated { get; set; } = string.Empty;
    }

    // ViewModel for individual life area scores
    public class LifeAreaScoreViewModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public string AreaSlug { get; set; } = string.Empty;
        public string AreaIcon { get; set; } = string.Empty;
        public string AreaColor { get; set; } = string.Empty;
        public string AreaDescription { get; set; } = string.Empty;
        
        [Range(1, 10, ErrorMessage = "Score must be between 1 and 10")]
        public int CurrentScore { get; set; } = 5;
        
        public int OrderIndex { get; set; }
    }

    // ViewModel for saving wheel scores
    public class SaveWheelScoresRequestViewModel
    {
        [Required]
        public List<LifeAreaScoreViewModel> Scores { get; set; } = new();
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    // ViewModel for the response after saving scores
    public class SaveWheelScoresResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public int TotalScore { get; set; }
        public double AverageScore { get; set; }
        public int AreasCount { get; set; }
        public DateTime SavedAt { get; set; }
    }

    // ViewModel for wheel statistics (optional future use)
    public class WheelStatsViewModel
    {
        public int TotalAssessments { get; set; }
        public int TotalAreas { get; set; }
        public double OverallAverageScore { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<AreaStatViewModel> AreaStats { get; set; } = new();
        public ScoreDistributionViewModel ScoreDistribution { get; set; } = new();
    }

    // ViewModel for area-specific statistics
    public class AreaStatViewModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public double AverageScore { get; set; }
        public int HighestScore { get; set; }
        public int LowestScore { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    // ViewModel for score distribution analysis
    public class ScoreDistributionViewModel
    {
        public int ScoresBelow5 { get; set; }
        public int ScoresAtMedium { get; set; } // 5-7
        public int ScoresAbove7 { get; set; }
        public List<string> StrongestAreas { get; set; } = new();
        public List<string> WeakestAreas { get; set; } = new();
    }

    // ViewModel for Chart.js data structure
    public class ChartDataViewModel
    {
        public List<string> Labels { get; set; } = new();
        public List<int> Data { get; set; } = new();
        public List<string> BackgroundColors { get; set; } = new();
        public List<string> BorderColors { get; set; } = new();
        public string ChartType { get; set; } = "radar";
    }
}