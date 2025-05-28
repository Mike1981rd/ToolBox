using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToolBox.Interfaces;
using ToolBox.Models.ViewModels;

namespace ToolBox.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    [Authorize]
    public class NotificationsApiController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationPreferenceService _preferenceService;
        private readonly ILogger<NotificationsApiController> _logger;

        public NotificationsApiController(
            INotificationService notificationService, 
            INotificationPreferenceService preferenceService,
            ILogger<NotificationsApiController> logger)
        {
            _notificationService = notificationService;
            _preferenceService = preferenceService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the count of unread notifications for the current user
        /// </summary>
        /// <returns>The number of unread notifications</returns>
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                var count = await _notificationService.GetUnreadNotificationCountAsync(userId.Value);
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting unread notification count");
                return StatusCode(500, "An error occurred while getting unread notification count");
            }
        }

        /// <summary>
        /// Gets the latest unread notifications for the current user
        /// </summary>
        /// <param name="count">Number of notifications to retrieve (default: 5, max: 20)</param>
        /// <returns>List of unread notifications</returns>
        [HttpGet("latest-unread")]
        public async Task<IActionResult> GetLatestUnread([FromQuery] int count = 5)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                // Limit the count to prevent abuse
                if (count < 1) count = 5;
                if (count > 20) count = 20;

                var notifications = await _notificationService.GetUnreadNotificationsAsync(userId.Value, count);
                
                // Format the response for the frontend
                var formattedNotifications = notifications.Select(n => new
                {
                    id = n.Id,
                    type = n.NotificationType,
                    data = n.Data, // This is already JSON string
                    createdAt = n.CreatedAt,
                    isRead = n.ReadAt.HasValue,
                    readAt = n.ReadAt
                });

                return Ok(new { notifications = formattedNotifications });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting latest unread notifications");
                return StatusCode(500, "An error occurred while getting notifications");
            }
        }

        /// <summary>
        /// Gets all notifications for the current user with pagination
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page (default: 10, max: 50)</param>
        /// <returns>Paginated list of notifications</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                // Validate and limit pagination parameters
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 50) pageSize = 50;

                var notifications = await _notificationService.GetAllNotificationsAsync(userId.Value, pageNumber, pageSize);
                
                // Format the response for the frontend
                var formattedNotifications = notifications.Select(n => new
                {
                    id = n.Id,
                    type = n.NotificationType,
                    data = n.Data, // This is already JSON string
                    createdAt = n.CreatedAt,
                    isRead = n.ReadAt.HasValue,
                    readAt = n.ReadAt
                });

                return Ok(new 
                { 
                    notifications = formattedNotifications,
                    pagination = new
                    {
                        currentPage = pageNumber,
                        pageSize = pageSize
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all notifications");
                return StatusCode(500, "An error occurred while getting notifications");
            }
        }

        /// <summary>
        /// Marks a specific notification as read
        /// </summary>
        /// <param name="id">The notification ID</param>
        /// <returns>Success status</returns>
        [HttpPost("{id:long}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(long id)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                await _notificationService.MarkAsReadAsync(id, userId.Value);
                return Ok(new { success = true, message = "Notification marked as read" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid notification ID or unauthorized access");
                return NotFound(new { success = false, message = "Notification not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read");
                return StatusCode(500, "An error occurred while marking notification as read");
            }
        }

        /// <summary>
        /// Marks all notifications as read for the current user
        /// </summary>
        /// <returns>Success status</returns>
        [HttpPost("mark-all-as-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                await _notificationService.MarkAllAsReadAsync(userId.Value);
                return Ok(new { success = true, message = "All notifications marked as read" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking all notifications as read");
                return StatusCode(500, "An error occurred while marking notifications as read");
            }
        }

        /// <summary>
        /// Gets the notification preferences for the current user
        /// </summary>
        /// <returns>List of notification preferences</returns>
        [HttpGet("preferences")]
        public async Task<IActionResult> GetPreferences()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                var preferences = await _preferenceService.GetUserPreferencesAsync(userId.Value.ToString());
                return Ok(preferences);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notification preferences");
                return StatusCode(500, "An error occurred while getting notification preferences");
            }
        }

        /// <summary>
        /// Updates the notification preferences for the current user
        /// </summary>
        /// <param name="preferences">List of preferences to update</param>
        /// <returns>Success status</returns>
        [HttpPost("preferences")]
        public async Task<IActionResult> UpdatePreferences([FromBody] IEnumerable<NotificationPreferenceUpdateModel> preferences)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                await _preferenceService.UpdateUserPreferencesAsync(userId.Value.ToString(), preferences);
                return Ok(new { success = true, message = "Preferences updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating notification preferences");
                return StatusCode(500, "An error occurred while updating notification preferences");
            }
        }

        /// <summary>
        /// Gets the current user ID from the authentication claims
        /// </summary>
        /// <returns>User ID or null if not authenticated</returns>
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }

            // For development - return test user while login is not implemented
            // TODO: Remove this when real authentication is implemented
            if (User.Identity?.IsAuthenticated == true)
            {
                return 1; // Test user ID
            }

            return null;
        }
    }
}