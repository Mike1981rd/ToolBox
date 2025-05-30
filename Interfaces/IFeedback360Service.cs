using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface IFeedback360Service
    {
        // Instance management
        Task<IEnumerable<Feedback360Instance>> GetInstancesByCoachAsync(int coachId);
        Task<Feedback360Instance?> GetInstanceByIdAsync(long instanceId, bool includeRaters = false);
        Task<Feedback360Instance> CreateInstanceAsync(int subjectUserId, int coachId, string title, DateTime deadline);
        Task<bool> UpdateInstanceStatusAsync(long instanceId, Feedback360Status newStatus);
        
        // Rater management
        Task<Feedback360Rater?> AddRaterAsync(long instanceId, string email, string? name, RaterCategory category, int? existingUserId = null);
        Task<bool> RemoveRaterAsync(long raterId);
        Task<IEnumerable<Feedback360Rater>> GetRatersByInstanceAsync(long instanceId);
        Task<bool> CanRemoveRaterAsync(long raterId);
        
        // Process management
        Task<(bool Success, string Message)> FinalizeAndSendInvitationsAsync(long instanceId);
        Task<Feedback360Rater?> GetRaterByTokenAsync(string token);
        
        // Statistics
        Task<(int Total, int Completed)> GetRaterStatsAsync(long instanceId);
        
        // Report generation
        Task<Feedback360ReportViewModel> GenerateReportAsync(long instanceId, int requestingUserId);
        Task<bool> CanViewReportAsync(long instanceId, int userId, bool isCoach = true);
    }
}