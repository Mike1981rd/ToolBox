using System.Threading.Tasks;
using ToolBox.Models;

namespace ToolBox.Interfaces
{
    public interface IWebsiteConfigurationService
    {
        Task<WebsiteConfiguration> GetConfigurationAsync();
        Task<WebsiteConfiguration> UpdateConfigurationAsync(WebsiteConfiguration configuration);
        Task<bool> UpdateLogoAsync(string logoPath);
        Task<bool> ResetLogoAsync();
        Task<bool> UpdateUserDefaultLanguageAsync(int userId, string language);
        Task<string?> GetUserDefaultLanguageAsync(int userId);
    }
}