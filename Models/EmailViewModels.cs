using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class EmailListItemViewModel
    {
        public int Id { get; set; }
        public string SenderAvatar { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Preview { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public bool IsStarred { get; set; }
        public DateTime Date { get; set; }
        public int AttachmentsCount { get; set; }
        public List<string> Labels { get; set; } = new List<string>();
        public string Folder { get; set; } = "inbox";
        public bool IsImportant { get; set; }
    }

    public class EmailDetailViewModel
    {
        public int Id { get; set; }
        public string SenderAvatar { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string FullBody { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<string> To { get; set; } = new List<string>();
        public List<string> CC { get; set; } = new List<string>();
        public List<string> BCC { get; set; } = new List<string>();
        public List<string> Labels { get; set; } = new List<string>();
        public List<EmailAttachmentViewModel> Attachments { get; set; } = new List<EmailAttachmentViewModel>();
        public bool IsStarred { get; set; }
        public bool IsImportant { get; set; }
        public string Folder { get; set; } = "inbox";
    }

    public class ComposeEmailViewModel
    {
        [Required]
        public string To { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public string BCC { get; set; } = string.Empty;
        
        [Required]
        public string Subject { get; set; } = string.Empty;
        
        [Required]
        public string Body { get; set; } = string.Empty;
        public List<string> Labels { get; set; } = new List<string>();
        public bool SaveAsDraft { get; set; }
    }

    public class EmailAttachmentViewModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileSize { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
    }

    public class EmailFolderViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Count { get; set; }
        public string TranslateKey { get; set; } = string.Empty;
        public string CssClass { get; set; } = string.Empty;
    }

    public class EmailLabelViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string TranslateKey { get; set; } = string.Empty;
    }

    public class EmailIndexViewModel
    {
        public List<EmailListItemViewModel> Emails { get; set; } = new List<EmailListItemViewModel>();
        public List<EmailFolderViewModel> Folders { get; set; } = new List<EmailFolderViewModel>();
        public List<EmailLabelViewModel> Labels { get; set; } = new List<EmailLabelViewModel>();
        public string CurrentFolder { get; set; } = "inbox";
        public int TotalEmails { get; set; }
        public int UnreadCount { get; set; }
    }
}