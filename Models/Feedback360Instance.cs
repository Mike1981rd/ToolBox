using System;
using System.Collections.Generic;

namespace ToolBox.Models
{
    public class Feedback360Instance
    {
        public long Id { get; set; }
        
        // El cliente que es evaluado
        public int SubjectUserId { get; set; }
        public User? SubjectUser { get; set; }
        
        // El coach que inició el proceso
        public int InitiatedByCoachId { get; set; }
        public User? InitiatedByCoach { get; set; }
        
        public Feedback360Status Status { get; set; } = Feedback360Status.PendingSetup;
        
        public string InstanceTitle { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? FeedbackDeadline { get; set; }
        
        public DateTime? ReportGeneratedAt { get; set; }
        
        // Navigation properties
        public ICollection<Feedback360Rater> Raters { get; set; } = new List<Feedback360Rater>();
    }
}