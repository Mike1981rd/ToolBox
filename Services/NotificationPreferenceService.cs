using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class NotificationPreferenceService : INotificationPreferenceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotificationPreferenceService> _logger;

        public NotificationPreferenceService(ApplicationDbContext context, ILogger<NotificationPreferenceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<NotificationPreferenceViewModel>> GetUserPreferencesAsync(string userId)
        {
            if (!int.TryParse(userId, out int userIdInt))
            {
                _logger.LogWarning("Invalid userId format: {UserId}", userId);
                return Enumerable.Empty<NotificationPreferenceViewModel>();
            }

            // Get existing preferences from database
            var existingPreferences = await _context.NotificationPreferences
                .Where(np => np.UserId == userIdInt)
                .ToDictionaryAsync(np => np.NotificationType);

            // Build list with all defined notification types
            var allPreferences = new List<NotificationPreferenceViewModel>();

            foreach (var notificationType in DefinedNotificationTypes.GetAllTypeKeys())
            {
                if (existingPreferences.TryGetValue(notificationType, out var preference))
                {
                    // Use existing preference
                    allPreferences.Add(new NotificationPreferenceViewModel
                    {
                        NotificationType = notificationType,
                        NotificationTypeDisplayName = DefinedNotificationTypes.GetDisplayNameKey(notificationType),
                        IsEnabledInApp = preference.IsEnabledInApp,
                        IsEnabledEmail = preference.IsEnabledEmail
                    });
                }
                else
                {
                    // Use default values
                    allPreferences.Add(new NotificationPreferenceViewModel
                    {
                        NotificationType = notificationType,
                        NotificationTypeDisplayName = DefinedNotificationTypes.GetDisplayNameKey(notificationType),
                        IsEnabledInApp = true,  // Default: In-app notifications enabled
                        IsEnabledEmail = false  // Default: Email notifications disabled
                    });
                }
            }

            return allPreferences;
        }

        public async Task UpdateUserPreferencesAsync(string userId, IEnumerable<NotificationPreferenceUpdateModel> preferencesToUpdate)
        {
            if (!int.TryParse(userId, out int userIdInt))
            {
                _logger.LogWarning("Invalid userId format: {UserId}", userId);
                return;
            }

            // Get existing preferences
            var existingPreferences = await _context.NotificationPreferences
                .Where(np => np.UserId == userIdInt)
                .ToDictionaryAsync(np => np.NotificationType);

            foreach (var updateModel in preferencesToUpdate)
            {
                if (existingPreferences.TryGetValue(updateModel.NotificationType, out var preference))
                {
                    // Update existing preference
                    preference.IsEnabledInApp = updateModel.IsEnabledInApp;
                    preference.IsEnabledEmail = updateModel.IsEnabledEmail;
                    preference.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Create new preference
                    _context.NotificationPreferences.Add(new NotificationPreference
                    {
                        UserId = userIdInt,
                        NotificationType = updateModel.NotificationType,
                        IsEnabledInApp = updateModel.IsEnabledInApp,
                        IsEnabledEmail = updateModel.IsEnabledEmail,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated notification preferences for user {UserId}", userId);
        }

        public async Task<NotificationPreferenceViewModel?> GetUserPreferenceForTypeAsync(string userId, string notificationType)
        {
            if (!int.TryParse(userId, out int userIdInt))
            {
                _logger.LogWarning("Invalid userId format: {UserId}", userId);
                return null;
            }

            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(np => np.UserId == userIdInt && np.NotificationType == notificationType);

            // If no preference exists, return default values
            if (preference == null)
            {
                return new NotificationPreferenceViewModel
                {
                    NotificationType = notificationType,
                    NotificationTypeDisplayName = DefinedNotificationTypes.GetDisplayNameKey(notificationType),
                    IsEnabledInApp = true,
                    IsEnabledEmail = false
                };
            }

            return new NotificationPreferenceViewModel
            {
                NotificationType = preference.NotificationType,
                NotificationTypeDisplayName = DefinedNotificationTypes.GetDisplayNameKey(preference.NotificationType),
                IsEnabledInApp = preference.IsEnabledInApp,
                IsEnabledEmail = preference.IsEnabledEmail
            };
        }
    }
}