using System;
using System.Collections.Generic;
using ToolBox.Models;

namespace ToolBox.Models.ViewModels
{
    public class Feedback360ReportViewModel
    {
        public long InstanceId { get; set; }
        public string InstanceTitle { get; set; } = string.Empty;
        public string SubjectUserName { get; set; } = string.Empty;
        public DateTime ReportGeneratedAt { get; set; }
        public DateTime? FeedbackDeadline { get; set; }
        public int TotalRaters { get; set; }
        public int CompletedRaters { get; set; }
        
        public List<CompetencyReportViewModel> CompetencyReports { get; set; } = new List<CompetencyReportViewModel>();
        public List<OpenEndedReportViewModel> OpenEndedFeedback { get; set; } = new List<OpenEndedReportViewModel>();
        
        // Analysis results
        public List<string> IdentifiedStrengths { get; set; } = new List<string>();
        public List<string> IdentifiedDevelopmentAreas { get; set; } = new List<string>();
        public List<string> SignificantGaps { get; set; } = new List<string>();
        
        // Data for overall comparison charts
        public List<string> OverallComparisonLabels { get; set; } = new List<string>();
        public List<ReportSeriesData> OverallComparisonSeries { get; set; } = new List<ReportSeriesData>();
        
        // Permission controls
        public bool CanViewDetailedBreakdown { get; set; } = true;
        public bool IsCoachView { get; set; } = true;
    }

    public class CompetencyReportViewModel
    {
        public string CompetencyCode { get; set; } = string.Empty;
        public string CompetencyName { get; set; } = string.Empty;
        public string CompetencyDescription { get; set; } = string.Empty;
        
        public List<QuestionScaleReportViewModel> QuestionScaleReports { get; set; } = new List<QuestionScaleReportViewModel>();
        
        // Competency-level averages
        public Dictionary<RaterCategory, double?> CompetencyAveragesByCategory { get; set; } = new Dictionary<RaterCategory, double?>();
        public double? OverallCompetencyAverage { get; set; }
        
        // Data for competency-specific charts
        public List<string> CompetencyChartLabels { get; set; } = new List<string>(); // Question names
        public List<ReportSeriesData> CompetencyComparisonSeries { get; set; } = new List<ReportSeriesData>();
        
        // Analysis
        public bool IsStrength { get; set; }
        public bool IsDevelopmentArea { get; set; }
        public bool HasSignificantGap { get; set; }
        public string GapDescription { get; set; } = string.Empty;
    }

    public class QuestionScaleReportViewModel
    {
        public string QuestionCode { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        
        // Scores by rater category
        public Dictionary<RaterCategory, double?> ScoresByRaterCategory { get; set; } = new Dictionary<RaterCategory, double?>();
        public Dictionary<RaterCategory, int> ResponseCountByCategory { get; set; } = new Dictionary<RaterCategory, int>();
        
        // Overall statistics
        public double? OverallAverageScore { get; set; } // Excluding self
        public double? SelfScore { get; set; }
        public double? GapFromSelf { get; set; } // Difference between self and others
        
        // For anonymity protection
        public Dictionary<RaterCategory, bool> HasSufficientResponses { get; set; } = new Dictionary<RaterCategory, bool>();
    }

    public class OpenEndedReportViewModel
    {
        public string QuestionCode { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        
        // Anonymous responses grouped by category (when safe to do so)
        public List<AnonymousResponseGroup> ResponseGroups { get; set; } = new List<AnonymousResponseGroup>();
        
        // All responses mixed for maximum anonymity
        public List<string> AllAnonymousResponses { get; set; } = new List<string>();
        
        public int TotalResponseCount { get; set; }
    }

    public class AnonymousResponseGroup
    {
        public string CategoryLabel { get; set; } = string.Empty; // "Manager", "Colleagues", "Team Members", etc.
        public List<string> Responses { get; set; } = new List<string>();
        public bool IsSafeToDisplay { get; set; } // Based on minimum response threshold
    }

    public class ReportSeriesData
    {
        public string SeriesLabel { get; set; } = string.Empty; // "Self Assessment", "Manager", "Peers Average"
        public List<double?> Data { get; set; } = new List<double?>(); // Scores for each competency/question
        public string BackgroundColor { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public bool ShowInLegend { get; set; } = true;
    }

    // Helper class for report analysis
    public class ReportAnalysisData
    {
        public double StrengthThreshold { get; set; } = 4.0; // Scores >= this are strengths
        public double DevelopmentThreshold { get; set; } = 3.0; // Scores < this are development areas
        public double SignificantGapThreshold { get; set; } = 1.0; // Gap between self and others
        public int MinimumResponsesForDisplay { get; set; } = 3; // Minimum responses to show category breakdown
        public int MinimumResponsesForPeers { get; set; } = 3; // Minimum for peer categories
        public int MinimumResponsesForDirectReports { get; set; } = 2; // Minimum for direct reports
    }

    // Additional ViewModels specific to client reports
    public class ClientFeedback360ReportSummaryViewModel
    {
        public long Id { get; set; }
        public string InstanceTitle { get; set; } = string.Empty;
        public string CoachName { get; set; } = string.Empty;
        public Feedback360Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FeedbackDeadline { get; set; }
        public DateTime? ReportGeneratedAt { get; set; }
        public int TotalRaters { get; set; }
        public int CompletedRaters { get; set; }
        public bool IsReportAvailable { get; set; }
        public bool CanViewReport { get; set; }
        
        public string StatusDisplayName => Status switch
        {
            Feedback360Status.PendingSetup => "Setting Up",
            Feedback360Status.CollectingFeedback => "Collecting Feedback",
            Feedback360Status.Completed => "Completed",
            _ => Status.ToString()
        };
        
        public string StatusBadgeClass => Status switch
        {
            Feedback360Status.PendingSetup => "bg-warning",
            Feedback360Status.CollectingFeedback => "bg-info",
            Feedback360Status.Completed => "bg-success",
            _ => "bg-secondary"
        };
        
        public bool IsOverdue => Status == Feedback360Status.CollectingFeedback && 
                                FeedbackDeadline.HasValue && DateTime.UtcNow > FeedbackDeadline.Value;
        
        public string CompletionStatusText => $"{CompletedRaters}/{TotalRaters} completed";
        
        public double CompletionPercentage => TotalRaters > 0 ? 
            Math.Round((double)CompletedRaters / TotalRaters * 100, 1) : 0;
    }

    // Enum extensions for report display
    public static class RaterCategoryExtensions
    {
        public static string GetDisplayName(this RaterCategory category)
        {
            return category switch
            {
                RaterCategory.Self => "Self Assessment",
                RaterCategory.Manager => "Manager",
                RaterCategory.Peer => "Peers",
                RaterCategory.DirectReport => "Direct Reports",
                RaterCategory.Client => "Clients",
                RaterCategory.Other => "Others",
                _ => category.ToString()
            };
        }

        public static string GetChartColor(this RaterCategory category)
        {
            return category switch
            {
                RaterCategory.Self => "#3498db",      // Blue
                RaterCategory.Manager => "#e74c3c",   // Red
                RaterCategory.Peer => "#2ecc71",      // Green
                RaterCategory.DirectReport => "#f39c12", // Orange
                RaterCategory.Client => "#9b59b6",    // Purple
                RaterCategory.Other => "#95a5a6",     // Gray
                _ => "#34495e"                        // Dark gray
            };
        }
    }
}