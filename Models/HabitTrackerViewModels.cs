using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    /// <summary>
    /// Main ViewModel for the Habit Tracker page
    /// </summary>
    public class HabitTrackerPageViewModel
    {
        public List<HabitViewModel> Habits { get; set; } = new List<HabitViewModel>();
        public decimal OverallSuccessRate { get; set; }
        public int TotalHabits { get; set; }
        public int ActiveDays { get; set; }
        public string CurrentPeriod { get; set; } = "last7days";
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        
        // Properties for view compatibility
        public DateTime CurrentWeekStart { get; set; }
        public DateTime CurrentWeekEnd { get; set; }
    }

    /// <summary>
    /// ViewModel for individual habits
    /// </summary>
    public class HabitViewModel
    {
        public int HabitId { get; set; }
        
        [Required(ErrorMessage = "Habit name is required")]
        [StringLength(100, ErrorMessage = "Habit name cannot exceed 100 characters")]
        public string HabitName { get; set; } = string.Empty;
        
        public string IconOrColor { get; set; } = "#667eea";
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        
        public List<DailyStatusViewModel> DailyStatuses { get; set; } = new List<DailyStatusViewModel>();
        
        /// <summary>
        /// Number of days the habit was completed in the current period
        /// </summary>
        public int DaysMet { get; set; }
        
        /// <summary>
        /// Percentage of completion for the current period
        /// </summary>
        public decimal PercentageMet { get; set; }
        
        /// <summary>
        /// Current streak of consecutive days
        /// </summary>
        public int CurrentStreak { get; set; }
        
        /// <summary>
        /// Best streak achieved
        /// </summary>
        public int BestStreak { get; set; }

        // Properties for view compatibility
        public int Id => HabitId;
        public string Name => HabitName;
        public string Color => IconOrColor;
        public string Description { get; set; } = string.Empty;
        public List<DailyStatusViewModel> DailyStatus => DailyStatuses;
        public decimal CompletionPercentage => PercentageMet;
    }

    /// <summary>
    /// ViewModel for daily habit status
    /// </summary>
    public class DailyStatusViewModel
    {
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DayOfWeek DayOfWeek => Date.DayOfWeek;
        
        /// <summary>
        /// Returns the day name in short format (Mon, Tue, etc.)
        /// </summary>
        public string DayShortName 
        { 
            get 
            {
                return Date.ToString("ddd");
            }
        }
        
        /// <summary>
        /// Returns the day name for display based on day of week
        /// </summary>
        public string DayDisplayName
        {
            get
            {
                return DayOfWeek switch
                {
                    System.DayOfWeek.Monday => "Lun",
                    System.DayOfWeek.Tuesday => "Mar",
                    System.DayOfWeek.Wednesday => "Mié",
                    System.DayOfWeek.Thursday => "Jue",
                    System.DayOfWeek.Friday => "Vie",
                    System.DayOfWeek.Saturday => "Sáb",
                    System.DayOfWeek.Sunday => "Dom",
                    _ => "N/A"
                };
            }
        }

        // Properties for view compatibility
        public string DayName => Date.ToString("dddd", System.Globalization.CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// ViewModel for submitting habit log entries
    /// </summary>
    public class HabitLogEntryViewModel
    {
        public int HabitId { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// ViewModel for habit chart data
    /// </summary>
    public class HabitChartDataViewModel
    {
        public List<string> Labels { get; set; } = new List<string>();
        public List<HabitChartDatasetViewModel> Datasets { get; set; } = new List<HabitChartDatasetViewModel>();
        public string ChartTitle { get; set; } = "Habit Progress";
        public string Period { get; set; } = "last7days";
    }

    /// <summary>
    /// ViewModel for individual habit chart dataset
    /// </summary>
    public class HabitChartDatasetViewModel
    {
        public string Label { get; set; } = string.Empty;
        public List<decimal> Data { get; set; } = new List<decimal>();
        public string BackgroundColor { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public int BorderWidth { get; set; } = 2;
    }

    /// <summary>
    /// ViewModel for API responses
    /// </summary>
    public class HabitTrackerResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public HabitViewModel? Habit { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public DateTime ActionDate { get; set; }
        public object? Data { get; set; }
    }

    /// <summary>
    /// ViewModel for habit statistics
    /// </summary>
    public class HabitStatisticsViewModel
    {
        public int TotalHabits { get; set; }
        public int ActiveHabits { get; set; }
        public decimal AverageCompletion { get; set; }
        public int TotalCompletions { get; set; }
        public int BestStreak { get; set; }
        public int CurrentActiveStreaks { get; set; }
        public List<HabitPerformanceViewModel> TopPerformers { get; set; } = new List<HabitPerformanceViewModel>();
    }

    /// <summary>
    /// ViewModel for habit performance metrics
    /// </summary>
    public class HabitPerformanceViewModel
    {
        public string HabitName { get; set; } = string.Empty;
        public decimal CompletionRate { get; set; }
        public int CurrentStreak { get; set; }
        public string Color { get; set; } = string.Empty;
    }

    /// <summary>
    /// Available chart periods
    /// </summary>
    public enum ChartPeriod
    {
        Last7Days,
        Last30Days,
        ThisMonth,
        AllTime
    }

    /// <summary>
    /// ViewModel for period filter buttons
    /// </summary>
    public class PeriodFilterViewModel
    {
        public ChartPeriod Period { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public string CssClass { get; set; } = string.Empty;
    }
}