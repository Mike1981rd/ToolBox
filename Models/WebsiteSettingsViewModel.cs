using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class WebsiteSettingsViewModel
    {
        // General Site Information
        [EmailAddress]
        [StringLength(100)]
        public string? SiteEmail { get; set; }

        [StringLength(200)]
        public string? SiteAddress { get; set; }

        [Phone]
        [StringLength(20)]
        public string? SitePhone { get; set; }

        [StringLength(500)]
        public string? FooterMessage { get; set; }

        // Social Media Links
        [Url]
        [StringLength(200)]
        public string? FacebookUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? TwitterUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? GoogleUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? LinkedInUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? YouTubeUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? InstagramUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? TelegramUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? TikTokUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? DiscordUrl { get; set; }

        [Url]
        [StringLength(200)]
        public string? RedditUrl { get; set; }

        // Branding
        [StringLength(300)]
        public string? CurrentLogoPath { get; set; }

        // Additional properties for configuration
        [StringLength(100)]
        public string? SiteName { get; set; }

        [StringLength(500)]
        public string? SiteDescription { get; set; }

        [StringLength(100)]
        public string? TimeZone { get; set; }

        [StringLength(10)]
        public string? DefaultLanguage { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        // Helper method to get social media configurations
        public List<SocialMediaConfig> GetSocialMediaConfigs()
        {
            return new List<SocialMediaConfig>
            {
                new SocialMediaConfig { Name = "Facebook", Icon = "fab fa-facebook-f", Url = FacebookUrl, Key = "FacebookUrl" },
                new SocialMediaConfig { Name = "Twitter", Icon = "fab fa-twitter", Url = TwitterUrl, Key = "TwitterUrl" },
                new SocialMediaConfig { Name = "Google", Icon = "fab fa-google", Url = GoogleUrl, Key = "GoogleUrl" },
                new SocialMediaConfig { Name = "LinkedIn", Icon = "fab fa-linkedin-in", Url = LinkedInUrl, Key = "LinkedInUrl" },
                new SocialMediaConfig { Name = "YouTube", Icon = "fab fa-youtube", Url = YouTubeUrl, Key = "YouTubeUrl" },
                new SocialMediaConfig { Name = "Instagram", Icon = "fab fa-instagram", Url = InstagramUrl, Key = "InstagramUrl" },
                new SocialMediaConfig { Name = "Telegram", Icon = "fab fa-telegram-plane", Url = TelegramUrl, Key = "TelegramUrl" },
                new SocialMediaConfig { Name = "TikTok", Icon = "fab fa-tiktok", Url = TikTokUrl, Key = "TikTokUrl" },
                new SocialMediaConfig { Name = "Discord", Icon = "fab fa-discord", Url = DiscordUrl, Key = "DiscordUrl" },
                new SocialMediaConfig { Name = "Reddit", Icon = "fab fa-reddit-alien", Url = RedditUrl, Key = "RedditUrl" }
            };
        }
    }

    public class SocialMediaConfig
    {
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string Key { get; set; } = string.Empty;
    }
}