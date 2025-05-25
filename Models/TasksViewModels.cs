using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    /// <summary>
    /// Main ViewModel for the Tasks page
    /// </summary>
    public class TasksPageViewModel
    {
        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
        public TaskStatisticsViewModel Statistics { get; set; } = new TaskStatisticsViewModel();
    }

    /// <summary>
    /// ViewModel for individual tasks
    /// </summary>
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        
        [Required(ErrorMessage = "Task description is required")]
        [StringLength(500, ErrorMessage = "Task description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        public bool IsUrgent { get; set; } = false;
        public bool IsImportant { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        /// <summary>
        /// Determines which Eisenhower Matrix quadrant this task belongs to
        /// </summary>
        public EisenhowerQuadrant Quadrant 
        { 
            get 
            {
                if (IsUrgent && IsImportant)
                    return EisenhowerQuadrant.UrgentImportant;
                else if (!IsUrgent && IsImportant)
                    return EisenhowerQuadrant.NotUrgentImportant;
                else if (IsUrgent && !IsImportant)
                    return EisenhowerQuadrant.UrgentNotImportant;
                else
                    return EisenhowerQuadrant.NotUrgentNotImportant;
            }
        }
        
        /// <summary>
        /// Gets the action recommendation for this task based on its quadrant
        /// </summary>
        public string ActionRecommendation
        {
            get
            {
                return Quadrant switch
                {
                    EisenhowerQuadrant.UrgentImportant => "HACER",
                    EisenhowerQuadrant.NotUrgentImportant => "PROGRAMAR",
                    EisenhowerQuadrant.UrgentNotImportant => "DELEGAR",
                    EisenhowerQuadrant.NotUrgentNotImportant => "ELIMINAR",
                    _ => "REVISAR"
                };
            }
        }
        
        /// <summary>
        /// Gets category badges for task display
        /// </summary>
        public List<string> CategoryBadges
        {
            get
            {
                var badges = new List<string>();
                
                if (IsUrgent) badges.Add("URGENTE");
                if (IsImportant) badges.Add("IMPORTANTE");
                
                badges.Add(ActionRecommendation);
                
                return badges;
            }
        }
        
        /// <summary>
        /// Gets the quadrant color for UI display
        /// </summary>
        public string QuadrantColor
        {
            get
            {
                return Quadrant switch
                {
                    EisenhowerQuadrant.UrgentImportant => "#e53e3e",       // Red
                    EisenhowerQuadrant.NotUrgentImportant => "#38a169",    // Green
                    EisenhowerQuadrant.UrgentNotImportant => "#d69e2e",    // Yellow/Amber
                    EisenhowerQuadrant.NotUrgentNotImportant => "#4a5568", // Gray
                    _ => "#718096"
                };
            }
        }
        
        /// <summary>
        /// Gets the quadrant icon for UI display
        /// </summary>
        public string QuadrantIcon
        {
            get
            {
                return Quadrant switch
                {
                    EisenhowerQuadrant.UrgentImportant => "🔥",           // Fire
                    EisenhowerQuadrant.NotUrgentImportant => "📅",        // Calendar
                    EisenhowerQuadrant.UrgentNotImportant => "👥",        // People/Group
                    EisenhowerQuadrant.NotUrgentNotImportant => "🗑️",     // Trash
                    _ => "📋"
                };
            }
        }
    }

    /// <summary>
    /// Statistics about tasks for dashboard display
    /// </summary>
    public class TaskStatisticsViewModel
    {
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int UrgentImportantCount { get; set; }
        public int NotUrgentImportantCount { get; set; }
        public int UrgentNotImportantCount { get; set; }
        public int NotUrgentNotImportantCount { get; set; }
        
        public decimal CompletionPercentage => TotalTasks > 0 ? Math.Round((decimal)CompletedTasks / TotalTasks * 100, 0) : 0;
    }

    /// <summary>
    /// Eisenhower Matrix quadrants
    /// </summary>
    public enum EisenhowerQuadrant
    {
        UrgentImportant = 1,        // Do First
        NotUrgentImportant = 2,     // Schedule
        UrgentNotImportant = 3,     // Delegate
        NotUrgentNotImportant = 4   // Eliminate
    }

    /// <summary>
    /// ViewModel for API responses when saving tasks
    /// </summary>
    public class TaskResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public TaskViewModel? Task { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public DateTime ActionDate { get; set; }
    }

    /// <summary>
    /// ViewModel for quadrant data
    /// </summary>
    public class QuadrantViewModel
    {
        public EisenhowerQuadrant QuadrantType { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ActionTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = "#f8f9fa";
        public string BorderColor { get; set; } = "#dee2e6";
        public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
        public string CssClass { get; set; } = string.Empty;
        public string IconClass { get; set; } = string.Empty;
    }

    /// <summary>
    /// ViewModel for task creation/update forms
    /// </summary>
    public class TaskFormViewModel
    {
        public int? TaskId { get; set; }
        
        public string? Description { get; set; }
        
        public bool IsUrgent { get; set; } = false;
        public bool IsImportant { get; set; } = false;
    }
}