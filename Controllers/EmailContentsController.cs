using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class EmailContentsController : Controller
    {
        private readonly ILogger<EmailContentsController> _logger;

        public EmailContentsController(ILogger<EmailContentsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string folder = "inbox")
        {
            var model = new EmailIndexViewModel
            {
                CurrentFolder = folder,
                Folders = GetEmailFolders(),
                Labels = GetEmailLabels(),
                Emails = GetEmailsForFolder(folder)
            };

            model.TotalEmails = model.Emails.Count;
            model.UnreadCount = model.Emails.Count(e => !e.IsRead);

            return View(model);
        }

        public PartialViewResult _ComposeEmailModal()
        {
            var model = new ComposeEmailViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult SendEmail([FromBody] ComposeEmailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Simulate email sending
                    var emailId = new Random().Next(1000, 9999);
                    
                    // If saving as draft, add to drafts folder
                    if (model.SaveAsDraft)
                    {
                        return Json(new { 
                            success = true, 
                            message = "Email saved as draft successfully",
                            emailId = emailId,
                            isDraft = true
                        });
                    }
                    
                    return Json(new { 
                        success = true, 
                        message = "Email sent successfully",
                        emailId = emailId
                    });
                }
                
                return Json(new { success = false, message = "Please fill all required fields" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpGet]
        public JsonResult GetEmailDetail(int emailId)
        {
            try
            {
                var email = GetSampleEmailDetail(emailId);
                return Json(new { success = true, data = email });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult DeleteEmails([FromBody] List<int> emailIds)
        {
            try
            {
                // Simulate deletion
                return Json(new { 
                    success = true, 
                    message = $"Successfully deleted {emailIds.Count} email(s)",
                    deletedCount = emailIds.Count
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult StarEmail(int emailId, bool isStarred)
        {
            try
            {
                // Simulate starring/unstarring
                return Json(new { 
                    success = true, 
                    message = isStarred ? "Email starred" : "Email unstarred",
                    emailId = emailId,
                    isStarred = isStarred
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult MarkAsRead(int emailId, bool isRead)
        {
            try
            {
                // Simulate marking as read/unread
                return Json(new { 
                    success = true, 
                    message = isRead ? "Email marked as read" : "Email marked as unread",
                    emailId = emailId,
                    isRead = isRead
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult MoveToFolder([FromBody] Dictionary<string, object> data)
        {
            try
            {
                var emailIds = data["emailIds"] as List<int> ?? new List<int>();
                var targetFolder = data["targetFolder"]?.ToString() ?? "inbox";
                
                return Json(new { 
                    success = true, 
                    message = $"Successfully moved {emailIds.Count} email(s) to {targetFolder}",
                    movedCount = emailIds.Count,
                    targetFolder = targetFolder
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult ApplyLabel([FromBody] Dictionary<string, object> data)
        {
            try
            {
                var emailIds = data["emailIds"] as List<int> ?? new List<int>();
                var label = data["label"]?.ToString() ?? "";
                
                return Json(new { 
                    success = true, 
                    message = $"Successfully applied '{label}' label to {emailIds.Count} email(s)",
                    appliedCount = emailIds.Count,
                    label = label
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        private List<EmailFolderViewModel> GetEmailFolders()
        {
            return new List<EmailFolderViewModel>
            {
                new EmailFolderViewModel { Name = "inbox", Icon = "fas fa-inbox", Count = 12, TranslateKey = "email.inbox", CssClass = "text-primary" },
                new EmailFolderViewModel { Name = "sent", Icon = "fas fa-paper-plane", Count = 5, TranslateKey = "email.sent", CssClass = "" },
                new EmailFolderViewModel { Name = "draft", Icon = "fas fa-file-alt", Count = 3, TranslateKey = "email.draft", CssClass = "" },
                new EmailFolderViewModel { Name = "starred", Icon = "fas fa-star", Count = 8, TranslateKey = "email.starred", CssClass = "text-warning" },
                new EmailFolderViewModel { Name = "spam", Icon = "fas fa-exclamation-triangle", Count = 2, TranslateKey = "email.spam", CssClass = "text-warning" },
                new EmailFolderViewModel { Name = "trash", Icon = "fas fa-trash", Count = 1, TranslateKey = "email.trash", CssClass = "text-danger" }
            };
        }

        private List<EmailLabelViewModel> GetEmailLabels()
        {
            return new List<EmailLabelViewModel>
            {
                new EmailLabelViewModel { Name = "personal", Color = "#28a745", TranslateKey = "email.labels.personal" },
                new EmailLabelViewModel { Name = "company", Color = "#007bff", TranslateKey = "email.labels.company" },
                new EmailLabelViewModel { Name = "important", Color = "#dc3545", TranslateKey = "email.labels.important" },
                new EmailLabelViewModel { Name = "private", Color = "#6f42c1", TranslateKey = "email.labels.private" }
            };
        }

        private List<EmailListItemViewModel> GetEmailsForFolder(string folder)
        {
            var emails = new List<EmailListItemViewModel>
            {
                new EmailListItemViewModel
                {
                    Id = 1,
                    SenderAvatar = "/img/default-avatar.png",
                    SenderName = "John Doe",
                    SenderEmail = "john.doe@example.com",
                    Subject = "Project Update - Q4 2024",
                    Preview = "Hi team, I wanted to share the latest updates on our Q4 project milestones...",
                    IsRead = false,
                    IsStarred = true,
                    Date = DateTime.Now.AddHours(-2),
                    AttachmentsCount = 2,
                    Labels = new List<string> { "company", "important" },
                    Folder = "inbox"
                },
                new EmailListItemViewModel
                {
                    Id = 2,
                    SenderAvatar = "/img/default-avatar.png",
                    SenderName = "Sarah Wilson",
                    SenderEmail = "sarah@company.com",
                    Subject = "Meeting Reminder",
                    Preview = "Don't forget about our meeting tomorrow at 2 PM to discuss the new features...",
                    IsRead = true,
                    IsStarred = false,
                    Date = DateTime.Now.AddHours(-5),
                    AttachmentsCount = 0,
                    Labels = new List<string> { "company" },
                    Folder = "inbox"
                },
                new EmailListItemViewModel
                {
                    Id = 3,
                    SenderAvatar = "/img/default-avatar.png",
                    SenderName = "Marketing Team",
                    SenderEmail = "marketing@company.com",
                    Subject = "Newsletter - Weekly Updates",
                    Preview = "This week's highlights include new product launches, customer testimonials...",
                    IsRead = false,
                    IsStarred = false,
                    Date = DateTime.Now.AddDays(-1),
                    AttachmentsCount = 1,
                    Labels = new List<string> { "company" },
                    Folder = "inbox"
                },
                new EmailListItemViewModel
                {
                    Id = 4,
                    SenderAvatar = "/img/default-avatar.png",
                    SenderName = "Support Team",
                    SenderEmail = "support@toolbox.com",
                    Subject = "Welcome to ToolBox!",
                    Preview = "Thank you for joining ToolBox. Here's everything you need to get started...",
                    IsRead = true,
                    IsStarred = true,
                    Date = DateTime.Now.AddDays(-2),
                    AttachmentsCount = 0,
                    Labels = new List<string> { "important", "personal" },
                    Folder = "inbox"
                },
                new EmailListItemViewModel
                {
                    Id = 5,
                    SenderAvatar = "/img/default-avatar.png", 
                    SenderName = "Admin System",
                    SenderEmail = "noreply@toolbox.com",
                    Subject = "System Maintenance Notification",
                    Preview = "We will be performing scheduled maintenance on our servers this weekend...",
                    IsRead = false,
                    IsStarred = false,
                    Date = DateTime.Now.AddHours(-8),
                    AttachmentsCount = 0,
                    Labels = new List<string> { "important" },
                    Folder = "inbox"
                }
            };

            return emails.Where(e => e.Folder == folder).ToList();
        }

        private EmailDetailViewModel GetSampleEmailDetail(int emailId)
        {
            return new EmailDetailViewModel
            {
                Id = emailId,
                SenderAvatar = "/img/default-avatar.png",
                SenderName = "John Doe",
                SenderEmail = "john.doe@example.com",
                Subject = "Project Update - Q4 2024",
                FullBody = @"<p>Hi team,</p>
                           <p>I wanted to share the latest updates on our Q4 project milestones and deliverables.</p>
                           <p>Key achievements this week:</p>
                           <ul>
                               <li>Completed user interface redesign</li>
                               <li>Implemented new authentication system</li>
                               <li>Fixed critical bugs in the payment module</li>
                           </ul>
                           <p>Looking forward to your feedback.</p>
                           <p>Best regards,<br>John</p>",
                Date = DateTime.Now.AddHours(-2),
                To = new List<string> { "team@company.com" },
                CC = new List<string> { "manager@company.com" },
                Labels = new List<string> { "company", "important" },
                IsStarred = true,
                Attachments = new List<EmailAttachmentViewModel>
                {
                    new EmailAttachmentViewModel { Id = 1, FileName = "project-report.pdf", FileSize = "2.3 MB", FileType = "pdf" },
                    new EmailAttachmentViewModel { Id = 2, FileName = "screenshots.zip", FileSize = "5.1 MB", FileType = "zip" }
                }
            };
        }
    }
}