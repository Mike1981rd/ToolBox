using System.Collections.Generic;
using System.Linq;

namespace ToolBox.Models
{
    public static class DefinedNotificationTypes
    {
        public static readonly Dictionary<string, string> Types = new Dictionary<string, string>
        {
            { "session_scheduled_by_coach", "notification_type_session_scheduled" },
            { "calendar_event_invitation", "notification_type_calendar_invitation" },
            { "task_reminder", "notification_type_task_reminder" },
            { "task_completed", "notification_type_task_completed" },
            { "progress_update", "notification_type_progress_update" },
            { "habit_reminder", "notification_type_habit_reminder" },
            { "achievement_unlocked", "notification_type_achievement_unlocked" },
            { "questionnaire_assigned", "notification_type_questionnaire_assigned" },
            { "questionnaire_completed_by_client", "notification_type_questionnaire_completed" },
            { "communication_wheel_assigned", "notification_type_communication_wheel_assigned" },
            { "communication_wheel_completed", "notification_type_communication_wheel_completed" }
        };

        public static IEnumerable<string> GetAllTypeKeys() => Types.Keys;

        public static string GetDisplayNameKey(string notificationType)
        {
            return Types.TryGetValue(notificationType, out var displayNameKey) 
                ? displayNameKey 
                : notificationType;
        }
    }
}