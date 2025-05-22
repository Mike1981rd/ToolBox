namespace ToolBox.Models
{
    public enum DefaultVideoType
    {
        None,
        Uploaded,
        YouTube,
        Vimeo
    }

    public class WelcomeMessageViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? UploadedVideoPath { get; set; }
        public string? YouTubeUrl { get; set; }
        public string? VimeoUrl { get; set; }
        public DefaultVideoType VideoType { get; set; } = DefaultVideoType.None;
        public IFormFile? VideoFile { get; set; }
    }

    public class WelcomeMessageEditViewModel
    {
        public WelcomeMessageViewModel WelcomeMessage { get; set; } = new();
        public bool IsEditMode { get; set; } = true;
    }
}