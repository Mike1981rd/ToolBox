using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationPreferenceService _preferenceService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            ApplicationDbContext context,
            INotificationPreferenceService preferenceService,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<NotificationService> logger)
        {
            _context = context;
            _preferenceService = preferenceService;
            _emailSender = emailSender;
            _configuration = configuration;
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

                // Check user preferences and send email if enabled
                await SendEmailNotificationIfEnabled(userId, notificationType, dataPayload);
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

        private async Task SendEmailNotificationIfEnabled(int userId, string notificationType, object dataPayload)
        {
            try
            {
                var userPreference = await _preferenceService.GetUserPreferenceForTypeAsync(userId.ToString(), notificationType);
                
                if (userPreference != null && userPreference.IsEnabledEmail)
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user != null && !string.IsNullOrWhiteSpace(user.Email))
                    {
                        var displayNameKey = DefinedNotificationTypes.GetDisplayNameKey(notificationType);
                        string emailSubject = GenerateEmailSubject(notificationType, dataPayload, displayNameKey, user.DefaultLanguage ?? "es");
                        string emailBodyHtml = GenerateEmailBodyHtml(notificationType, dataPayload, user, displayNameKey, user.DefaultLanguage ?? "es");

                        if (!string.IsNullOrWhiteSpace(emailBodyHtml))
                        {
                            try
                            {
                                await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBodyHtml);
                                _logger.LogInformation("Email sent to {Email} for notification type {NotificationType}", 
                                    user.Email, notificationType);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Failed to send email to {Email} for notification type {NotificationType}", 
                                    user.Email, notificationType);
                            }
                        }
                    }
                    else
                    {
                        _logger.LogWarning("User {UserId} or their email is null/empty. Cannot send email for notification type {NotificationType}", 
                            userId, notificationType);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking email preferences for user {UserId}, notification type {NotificationType}", 
                    userId, notificationType);
            }
        }

        private string GenerateEmailSubject(string notificationType, object dataPayload, string notificationTypeDisplayName, string userLanguage = "es")
        {
            var appName = _configuration["AppName"] ?? "ToolBox";
            
            // Generate specific subjects based on notification type and user language
            switch (notificationType)
            {
                case "session_scheduled_by_coach":
                    return userLanguage == "en" 
                        ? $"[{appName}] New session scheduled"
                        : $"[{appName}] Nueva sesión programada";
                case "calendar_event_invitation":
                    return userLanguage == "en" 
                        ? $"[{appName}] Calendar event invitation"
                        : $"[{appName}] Invitación a evento del calendario";
                default:
                    return userLanguage == "en" 
                        ? $"[{appName}] New notification"
                        : $"[{appName}] Nueva notificación";
            }
        }

        private string GenerateEmailBodyHtml(string notificationType, object dataPayload, User user, string notificationTypeDisplayName, string userLanguage = "es")
        {
            var jsonString = JsonSerializer.Serialize(dataPayload);
            using var payload = JsonDocument.Parse(jsonString);
            var root = payload.RootElement;
            
            string messageBody = "";
            var appName = _configuration["AppName"] ?? "ToolBox";
            var appBaseUrl = _configuration["AppBaseUrl"] ?? "https://localhost:5001";

            switch (notificationType)
            {
                case "session_scheduled_by_coach":
                    var sessionId = root.TryGetProperty("SessionId", out var sessionIdElement) ? sessionIdElement.GetString() : null;
                    var sessionDateTime = root.TryGetProperty("SessionDateTime", out var dateElement) && dateElement.TryGetDateTime(out var dateTime) ? dateTime : (DateTime?)null;
                    var coachName = root.TryGetProperty("CoachName", out var coachNameElement) ? coachNameElement.GetString() : null;
                    
                    if (userLanguage == "en")
                    {
                        messageBody = $@"
                            <h2>Hello {user.FullName},</h2>
                            <p>A new session has been scheduled for you.</p>
                            <p><strong>Coach:</strong> {coachName}<br/>
                            <strong>Date and time:</strong> {sessionDateTime?.ToString("dddd, MMMM dd, yyyy HH:mm")}</p>";
                        
                        if (!string.IsNullOrEmpty(sessionId))
                        {
                            var actionUrl = $"{appBaseUrl.TrimEnd('/')}/Sessions/Details/{sessionId}";
                            messageBody += $@"<p><a href='{actionUrl}' style='background-color: #7367f0; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;'>View session details</a></p>";
                        }
                    }
                    else
                    {
                        messageBody = $@"
                            <h2>Hola {user.FullName},</h2>
                            <p>Se ha programado una nueva sesión para ti.</p>
                            <p><strong>Coach:</strong> {coachName}<br/>
                            <strong>Fecha y hora:</strong> {sessionDateTime?.ToString("dddd, dd MMMM yyyy HH:mm")}</p>";
                        
                        if (!string.IsNullOrEmpty(sessionId))
                        {
                            var actionUrl = $"{appBaseUrl.TrimEnd('/')}/Sessions/Details/{sessionId}";
                            messageBody += $@"<p><a href='{actionUrl}' style='background-color: #7367f0; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;'>Ver detalles de la sesión</a></p>";
                        }
                    }
                    break;

                case "calendar_event_invitation":
                    var eventId = root.TryGetProperty("EventId", out var eventIdElement) ? eventIdElement.GetString() : null;
                    var eventTitle = root.TryGetProperty("EventTitle", out var eventTitleElement) ? eventTitleElement.GetString() : null;
                    var eventStartDate = root.TryGetProperty("EventStartDate", out var eventDateElement) && eventDateElement.TryGetDateTime(out var eventDateTime) ? eventDateTime : (DateTime?)null;
                    var invitedBy = root.TryGetProperty("InvitedBy", out var invitedByElement) ? invitedByElement.GetString() : null;
                    
                    if (userLanguage == "en")
                    {
                        messageBody = $@"
                            <h2>Hello {user.FullName},</h2>
                            <p>You have been invited to a calendar event.</p>
                            <p><strong>Event:</strong> {eventTitle}<br/>
                            <strong>Invited by:</strong> {invitedBy}<br/>
                            <strong>Start date:</strong> {eventStartDate?.ToString("dddd, MMMM dd, yyyy HH:mm")}</p>";
                        
                        if (!string.IsNullOrEmpty(eventId))
                        {
                            var actionUrl = $"{appBaseUrl.TrimEnd('/')}/Calendario";
                            messageBody += $@"<p><a href='{actionUrl}' style='background-color: #7367f0; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;'>View calendar</a></p>";
                        }
                    }
                    else
                    {
                        messageBody = $@"
                            <h2>Hola {user.FullName},</h2>
                            <p>Has sido invitado a un evento del calendario.</p>
                            <p><strong>Evento:</strong> {eventTitle}<br/>
                            <strong>Invitado por:</strong> {invitedBy}<br/>
                            <strong>Fecha de inicio:</strong> {eventStartDate?.ToString("dddd, dd MMMM yyyy HH:mm")}</p>";
                        
                        if (!string.IsNullOrEmpty(eventId))
                        {
                            var actionUrl = $"{appBaseUrl.TrimEnd('/')}/Calendario";
                            messageBody += $@"<p><a href='{actionUrl}' style='background-color: #7367f0; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;'>Ver calendario</a></p>";
                        }
                    }
                    break;

                default:
                    if (userLanguage == "en")
                    {
                        messageBody = $@"
                            <h2>Hello {user.FullName},</h2>
                            <p>You have a new notification in {appName}.</p>";
                    }
                    else
                    {
                        messageBody = $@"
                            <h2>Hola {user.FullName},</h2>
                            <p>Tienes una nueva notificación en {appName}.</p>";
                    }
                    break;
            }

            // Email template
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }}
        .container {{
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        .header {{
            background-color: #7367f0;
            color: white;
            padding: 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 30px;
        }}
        .footer {{
            background-color: #f8f9fa;
            padding: 20px;
            text-align: center;
            font-size: 14px;
            color: #666;
        }}
        a {{
            color: #7367f0;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{appName}</h1>
        </div>
        <div class='content'>
            {messageBody}
            <hr style='border: none; border-top: 1px solid #eee; margin: 30px 0;'>
            <p style='font-size: 14px; color: #666;'>
                {(userLanguage == "en" 
                    ? $"If you want to change your notification preferences, visit your <a href='{appBaseUrl.TrimEnd('/')}/NotificationsMvc/Preferences'>account settings</a>." 
                    : $"Si deseas cambiar tus preferencias de notificación, visita la <a href='{appBaseUrl.TrimEnd('/')}/NotificationsMvc/Preferences'>configuración de tu cuenta</a>.")}
            </p>
        </div>
        <div class='footer'>
            <p>{(userLanguage == "en" ? $"Thanks,<br>The {appName} team" : $"Gracias,<br>El equipo de {appName}")}</p>
            <p style='font-size: 12px;'>
                {(userLanguage == "en" 
                    ? "This is an automated email, please do not reply to this message." 
                    : "Este es un correo automático, por favor no respondas a este mensaje.")}
            </p>
        </div>
    </div>
</body>
</html>";
        }
    }
}