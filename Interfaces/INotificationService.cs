using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface INotificationService
    {
        /// <summary>
        /// Creates a new notification for a user
        /// </summary>
        /// <param name="userId">The ID of the user receiving the notification</param>
        /// <param name="notificationType">Type of notification (e.g., "session_created_for_client")</param>
        /// <param name="dataPayload">Additional data to be serialized as JSON</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task CreateNotificationAsync(int userId, string notificationType, object dataPayload);

        /// <summary>
        /// Gets the most recent unread notifications for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="count">Number of notifications to retrieve (default: 5)</param>
        /// <returns>Collection of unread notifications</returns>
        Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId, int count = 5);

        /// <summary>
        /// Gets all notifications for a user with pagination
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of notifications per page</param>
        /// <returns>Collection of notifications</returns>
        Task<IEnumerable<Notification>> GetAllNotificationsAsync(int userId, int pageNumber, int pageSize);

        /// <summary>
        /// Gets the count of unread notifications for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>Number of unread notifications</returns>
        Task<int> GetUnreadNotificationCountAsync(int userId);

        /// <summary>
        /// Marks a specific notification as read
        /// </summary>
        /// <param name="notificationId">The ID of the notification</param>
        /// <param name="userId">The ID of the user (for security validation)</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task MarkAsReadAsync(long notificationId, int userId);

        /// <summary>
        /// Marks all notifications as read for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task MarkAllAsReadAsync(int userId);
    }
}