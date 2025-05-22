using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    /// <summary>
    /// Main ViewModel for the Wheel of Progress page
    /// </summary>
    public class WheelOfProgressPageViewModel
    {
        public List<AreaProgressViewModel> LifeAreas { get; set; } = new List<AreaProgressViewModel>();
        public decimal GlobalProgressPercentage { get; set; }
        public int TotalCategories { get; set; }
        public int CategoriesWithGoals { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    /// <summary>
    /// ViewModel for each Life Area with its categories and chart data
    /// </summary>
    public class AreaProgressViewModel
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public string AreaSlug { get; set; } = string.Empty;
        public string AreaColor { get; set; } = "#667eea";
        public string AreaIcon { get; set; } = "🎯";
        public int AreaScore { get; set; } // For chart display (from Wheel of Life)
        public List<CategoryProgressViewModel> Categories { get; set; } = new List<CategoryProgressViewModel>();
        public decimal AreaAverageProgress { get; set; } // Average progress of categories in this area
        public int OrderIndex { get; set; }
    }

    /// <summary>
    /// ViewModel for individual categories within a life area
    /// </summary>
    public class CategoryProgressViewModel
    {
        public int CategoryId { get; set; }
        public int AreaId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = "#f8f9fa";
        
        [Display(Name = "Goal")]
        public string GoalText { get; set; } = string.Empty;
        
        [Display(Name = "Progress %")]
        [Range(0, 100, ErrorMessage = "Progress must be between 0 and 100")]
        public int ProgressPercentage { get; set; } = 0;
        
        [Display(Name = "Next Action")]
        public string NextActionText { get; set; } = string.Empty;
        
        [Display(Name = "Deadline")]
        [DataType(DataType.Date)]
        public DateTime? DeadlineDate { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int OrderIndex { get; set; }
    }

    /// <summary>
    /// ViewModel for submitting progress data updates
    /// </summary>
    public class ProgressItemViewModel
    {
        public int CategoryId { get; set; }
        public int AreaId { get; set; }
        
        [Required(ErrorMessage = "Goal is required")]
        [StringLength(500, ErrorMessage = "Goal cannot exceed 500 characters")]
        public string GoalText { get; set; } = string.Empty;
        
        [Range(0, 100, ErrorMessage = "Progress must be between 0 and 100")]
        public int ProgressPercentage { get; set; } = 0;
        
        [StringLength(500, ErrorMessage = "Next action cannot exceed 500 characters")]
        public string NextActionText { get; set; } = string.Empty;
        
        public DateTime? DeadlineDate { get; set; }
    }

    /// <summary>
    /// ViewModel for API responses when saving progress
    /// </summary>
    public class SaveProgressResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public decimal NewGlobalProgressPercentage { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public DateTime SavedAt { get; set; }
        public int TotalCategoriesUpdated { get; set; }
    }

    /// <summary>
    /// ViewModel for chart data specifically
    /// </summary>
    public class ProgressChartDataViewModel
    {
        public List<string> AreaNames { get; set; } = new List<string>();
        public List<int> AreaScores { get; set; } = new List<int>();
        public List<string> AreaColors { get; set; } = new List<string>();
        public decimal GlobalProgressPercentage { get; set; }
        public string ChartTitle { get; set; } = "Wheel of Progress";
    }
}