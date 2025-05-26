using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;

namespace ToolBox.Services
{
    public class WebsiteConfigurationService : IWebsiteConfigurationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WebsiteConfigurationService> _logger;

        public WebsiteConfigurationService(ApplicationDbContext context, ILogger<WebsiteConfigurationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<WebsiteConfiguration> GetConfigurationAsync()
        {
            var configuration = await _context.WebsiteConfiguration
                .Include(w => w.LastUpdatedByUser)
                .FirstOrDefaultAsync();

            if (configuration == null)
            {
                // Create default configuration if none exists
                configuration = new WebsiteConfiguration
                {
                    SiteName = "Toolbox",
                    SiteDescription = "Your personal development platform",
                    DefaultLanguage = "es",
                    DefaultTimeZone = "America/Mexico_City",
                    LastUpdated = DateTime.UtcNow
                };

                _context.WebsiteConfiguration.Add(configuration);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created default website configuration");
            }

            return configuration;
        }

        public async Task<WebsiteConfiguration> UpdateConfigurationAsync(WebsiteConfiguration configuration)
        {
            var existingConfig = await _context.WebsiteConfiguration.FirstOrDefaultAsync();
            
            if (existingConfig == null)
            {
                existingConfig = new WebsiteConfiguration();
                _context.WebsiteConfiguration.Add(existingConfig);
            }

            // Update all fields
            existingConfig.SiteEmail = configuration.SiteEmail;
            existingConfig.SitePhone = configuration.SitePhone;
            existingConfig.SiteAddress = configuration.SiteAddress;
            existingConfig.FooterMessage = configuration.FooterMessage;
            
            // Social media
            existingConfig.FacebookUrl = configuration.FacebookUrl;
            existingConfig.TwitterUrl = configuration.TwitterUrl;
            existingConfig.GoogleUrl = configuration.GoogleUrl;
            existingConfig.LinkedInUrl = configuration.LinkedInUrl;
            existingConfig.YouTubeUrl = configuration.YouTubeUrl;
            existingConfig.InstagramUrl = configuration.InstagramUrl;
            existingConfig.TelegramUrl = configuration.TelegramUrl;
            existingConfig.TikTokUrl = configuration.TikTokUrl;
            existingConfig.DiscordUrl = configuration.DiscordUrl;
            existingConfig.RedditUrl = configuration.RedditUrl;
            
            // Branding (logo path is handled separately)
            existingConfig.SiteName = configuration.SiteName;
            existingConfig.SiteDescription = configuration.SiteDescription;
            
            // Additional settings
            existingConfig.DefaultTimeZone = configuration.DefaultTimeZone;
            existingConfig.DefaultLanguage = configuration.DefaultLanguage;
            
            // Audit
            existingConfig.LastUpdated = DateTime.UtcNow;
            existingConfig.LastUpdatedByUserId = configuration.LastUpdatedByUserId;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated website configuration");
            
            return existingConfig;
        }

        public async Task<bool> UpdateLogoAsync(string logoPath)
        {
            var configuration = await GetConfigurationAsync();
            configuration.LogoPath = logoPath;
            configuration.LastUpdated = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ResetLogoAsync()
        {
            var configuration = await GetConfigurationAsync();
            configuration.LogoPath = null;
            configuration.LastUpdated = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserDefaultLanguageAsync(int userId, string language)
        {
            try
            {
                _logger.LogInformation("Attempting to update default language for user {UserId} to {Language}", userId, language);
                
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    return false;
                }

                _logger.LogInformation("Found user {UserId}, current DefaultLanguage: {CurrentLanguage}", userId, user.DefaultLanguage);
                
                user.DefaultLanguage = language;
                user.UpdatedAt = DateTime.UtcNow;
                
                var changes = await _context.SaveChangesAsync();
                _logger.LogInformation("SaveChanges returned {Changes} changes for user {UserId}", changes, userId);
                
                if (changes > 0)
                {
                    _logger.LogInformation("Successfully updated default language for user {UserId} to {Language}", userId, language);
                    return true;
                }
                else
                {
                    _logger.LogWarning("No changes were saved when updating language for user {UserId}", userId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating default language for user {UserId} to {Language}", userId, language);
                return false;
            }
        }

        public async Task<string?> GetUserDefaultLanguageAsync(int userId)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == userId)
                    .Select(u => u.DefaultLanguage)
                    .FirstOrDefaultAsync();
                
                return user ?? "es"; // Default fallback
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting default language for user {UserId}", userId);
                return "es"; // Default fallback
            }
        }
    }
}