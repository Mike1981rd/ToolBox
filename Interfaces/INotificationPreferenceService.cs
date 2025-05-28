using System.Collections.Generic;
using System.Threading.Tasks;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Interfaces
{
    public interface INotificationPreferenceService
    {
        Task<IEnumerable<NotificationPreferenceViewModel>> GetUserPreferencesAsync(string userId);
        Task UpdateUserPreferencesAsync(string userId, IEnumerable<NotificationPreferenceUpdateModel> preferencesToUpdate);
        Task<NotificationPreferenceViewModel?> GetUserPreferenceForTypeAsync(string userId, string notificationType);
    }
}