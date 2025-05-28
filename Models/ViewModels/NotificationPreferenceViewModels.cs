using System.Collections.Generic;

namespace ToolBox.Models.ViewModels
{
    public class NotificationPreferenceViewModel
    {
        public string NotificationType { get; set; } = string.Empty;
        public string NotificationTypeDisplayName { get; set; } = string.Empty;
        public bool IsEnabledInApp { get; set; }
        public bool IsEnabledEmail { get; set; }
    }

    public class NotificationPreferenceUpdateModel
    {
        public string NotificationType { get; set; } = string.Empty;
        public bool IsEnabledInApp { get; set; }
        public bool IsEnabledEmail { get; set; }
    }

    public class NotificationPreferencesPageViewModel
    {
        public List<NotificationPreferenceViewModel> Preferences { get; set; } = new List<NotificationPreferenceViewModel>();
    }
}