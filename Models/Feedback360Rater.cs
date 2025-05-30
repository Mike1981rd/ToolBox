using System;
using System.Collections.Generic;

namespace ToolBox.Models
{
    public class Feedback360Rater
    {
        public long Id { get; set; }
        
        public long Feedback360InstanceId { get; set; }
        public Feedback360Instance? Feedback360Instance { get; set; }
        
        // Si el evaluador es un usuario del sistema
        public int? RaterUserId { get; set; }
        public User? RaterUser { get; set; }
        
        // Si el evaluador es externo
        public string? ExternalRaterEmail { get; set; }
        public string? ExternalRaterName { get; set; }
        
        public RaterCategory Category { get; set; }
        
        // Token único para el enlace de respuesta
        public string UniqueResponseToken { get; set; } = Guid.NewGuid().ToString("N");
        
        public RaterStatus Status { get; set; } = RaterStatus.PendingInvitation;
        
        public DateTime? InvitedAt { get; set; }
        public DateTime? LastReminderSentAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        
        // Navigation properties
        public ICollection<Feedback360ResponseScale> ScaleResponses { get; set; } = new List<Feedback360ResponseScale>();
        public ICollection<Feedback360ResponseOpen> OpenEndedResponses { get; set; } = new List<Feedback360ResponseOpen>();
    }
}