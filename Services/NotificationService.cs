using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ApplicationDbContext context, ILogger<NotificationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateNotificationAsync(int userId, string notificationType, object dataPayload)
        {
            try
            {
                // Validate that user exists
                var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
                if (!userExists)
                {
                    _logger.LogWarning("Attempted to create notification for non-existent user {UserId}", userId);
                    throw new ArgumentException($"User with ID {userId} does not exist");
                }

                // Serialize the data payload to JSON
                var dataJson = JsonSerializer.Serialize(dataPayload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var notification = new Notification
                {
                    UserId = userId,
                    NotificationType = notificationType,
                    Data = dataJson,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created notification {NotificationType} for user {UserId}", 
                    notificationType, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating notification {NotificationType} for user {UserId}", 
                    notificationType, userId);
                throw;
            }
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId, int count = 5)
        {
            try
            {
                var notifications = await _context.Notifications
                    .Where(n => n.UserId == userId && n.ReadAt == null)
                    .OrderByDescending(n => n.CreatedAt)
                    .Take(count)
                    .Include(n => n.User)
                    .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread notifications for user {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(int userId, int pageNumber, int pageSize)
        {
            try
            {
                // Validate pagination parameters
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 100) pageSize = 100; // Limit max page size

                var skip = (pageNumber - 1) * pageSize;

                var notifications = await _context.Notifications
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedAt)
                    .Skip(skip)
                    .Take(pageSize)
                    .Include(n => n.User)
                    .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all notifications for user {UserId}, page {PageNumber}", 
                    userId, pageNumber);
                throw;
            }
        }

        public async Task<int> GetUnreadNotificationCountAsync(int userId)
        {
            try
            {
                var count = await _context.Notifications
                    .Where(n => n.UserId == userId && n.ReadAt == null)
                    .CountAsync();

                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread notification count for user {UserId}", userId);
                throw;
            }
        }

        public async Task MarkAsReadAsync(long notificationId, int userId)
        {
            try
            {
                var notification = await _context.Notifications
                    .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);

                if (notification == null)
                {
                    _logger.LogWarning("Notification {NotificationId} not found for user {UserId}", 
                        notificationId, userId);
                    throw new ArgumentException($"Notification with ID {notificationId} not found for user {userId}");
                }

                if (notification.ReadAt == null)
                {
                    notification.ReadAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Marked notification {NotificationId} as read for user {UserId}", 
                        notificationId, userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification {NotificationId} as read for user {UserId}", 
                    notificationId, userId);
                throw;
            }
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            try
            {
                var unreadNotifications = await _context.Notifications
                    .Where(n => n.UserId == userId && n.ReadAt == null)
                    .ToListAsync();

                if (unreadNotifications.Any())
                {
                    var readAt = DateTime.UtcNow;
                    foreach (var notification in unreadNotifications)
                    {
                        notification.ReadAt = readAt;
                    }

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Marked {Count} notifications as read for user {UserId}", 
                        unreadNotifications.Count, userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read for user {UserId}", userId);
                throw;
            }
        }
    }
}