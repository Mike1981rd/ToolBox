using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models
{
    public class AcademyVideoItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EmbedUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryIcon { get; set; } = string.Empty;
        public string CategoryColor { get; set; } = string.Empty;
        public string PublishDateFormatted { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorAvatar { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public TimeSpan Duration { get; set; }
        public string DurationFormatted { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public string ViewCountFormatted { get; set; } = string.Empty;
        public bool IsVideoAvailable { get; set; } = true;
        public double Rating { get; set; }
        public int LikesCount { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class AcademyVideoDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EmbedUrl { get; set; } = string.Empty;
        public string FullDescriptionHtml { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string PublishDateFormatted { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public bool IsFeatured { get; set; }
        public TimeSpan Duration { get; set; }
        public string DurationFormatted { get; set; } = string.Empty;
        public int ViewCount { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<AcademyVideoItemViewModel> RelatedVideos { get; set; } = new List<AcademyVideoItemViewModel>();
    }

    public class CategoryFilterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int VideoCount { get; set; }
        public string IconClass { get; set; } = string.Empty;
        public string IconEmoji { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string BackgroundColor { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Description { get; set; } = string.Empty;
        public int FeaturedVideosCount { get; set; }
    }

    public class ToolboxAcademyIndexViewModel
    {
        public List<AcademyVideoItemViewModel> Videos { get; set; } = new List<AcademyVideoItemViewModel>();
        public List<CategoryFilterViewModel> Categories { get; set; } = new List<CategoryFilterViewModel>();
        public string CurrentCategorySlug { get; set; } = string.Empty;
        public string CurrentCategoryName { get; set; } = string.Empty;
        public int TotalVideos { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int VideosPerPage { get; set; } = 6;
        public bool HasFeaturedVideos { get; set; }
        public string SearchQuery { get; set; } = string.Empty;
        
        // Header Statistics
        public int FeaturedVideosCount { get; set; }
        public int TotalCategoriesCount { get; set; }
        public int TotalViewsCount { get; set; }
        public string TotalViewsFormatted { get; set; } = string.Empty;
        public TimeSpan TotalWatchTime { get; set; }
        public string TotalWatchTimeFormatted { get; set; } = string.Empty;
        
        // Quick Stats for Sidebar
        public AcademyStatsViewModel Stats { get; set; } = new AcademyStatsViewModel();
        
        // Header Content
        public string HeaderTitle { get; set; } = "Academia Toolbox";
        public string HeaderSubtitle { get; set; } = "Aprende las mejores prácticas y técnicas con nuestros videos educativos";
        public string HeaderDescription { get; set; } = "Desde desarrollo web hasta estrategia de negocio";
        
        // Featured Card
        public FeaturedCardViewModel FeaturedCard { get; set; } = new FeaturedCardViewModel();
        
        // Filter State
        public bool IsFiltered => !string.IsNullOrEmpty(CurrentCategorySlug) && CurrentCategorySlug != "all";
        public bool HasSearchQuery => !string.IsNullOrEmpty(SearchQuery);
    }

    public class AcademyStatsViewModel
    {
        public int TotalVideos { get; set; }
        public int FeaturedVideos { get; set; }
        public int TotalCategories { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalViews { get; set; }
        public string TotalViewsFormatted { get; set; } = string.Empty;
        public TimeSpan TotalDuration { get; set; }
        public string TotalDurationFormatted { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public double AverageRating { get; set; }
        public int TotalLikes { get; set; }
    }

    public class FeaturedCardViewModel
    {
        public string Title { get; set; } = "Videos Destacados";
        public int Count { get; set; }
        public string Description { get; set; } = "Contenido recomendado para ti";
        public string BackgroundColor { get; set; } = "#ff6b9d";
        public string TextColor { get; set; } = "#ffffff";
        public string IconClass { get; set; } = "fas fa-star";
        public List<AcademyVideoItemViewModel> FeaturedVideos { get; set; } = new List<AcademyVideoItemViewModel>();
    }

    public class VideoStatsViewModel
    {
        public int TotalVideos { get; set; }
        public int TotalCategories { get; set; }
        public int TotalViews { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public string TotalDurationFormatted { get; set; } = string.Empty;
        public int FeaturedVideos { get; set; }
        public DateTime LastVideoPublished { get; set; }
    }

    public static class VideoUrlHelper
    {
        /// <summary>
        /// Converts a YouTube watch URL to an embed URL
        /// </summary>
        public static string ConvertToEmbedUrl(string videoUrl)
        {
            if (string.IsNullOrEmpty(videoUrl))
                return string.Empty;

            try
            {
                // Handle YouTube URLs
                if (videoUrl.Contains("youtube.com/watch"))
                {
                    var uri = new Uri(videoUrl);
                    var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                    if (query.TryGetValue("v", out var videoId) && !string.IsNullOrEmpty(videoId))
                    {
                        return $"https://www.youtube.com/embed/{videoId}";
                    }
                }
                else if (videoUrl.Contains("youtu.be/"))
                {
                    var videoId = videoUrl.Split('/').LastOrDefault();
                    if (!string.IsNullOrEmpty(videoId))
                    {
                        return $"https://www.youtube.com/embed/{videoId}";
                    }
                }
                // Handle Vimeo URLs
                else if (videoUrl.Contains("vimeo.com/"))
                {
                    var videoId = videoUrl.Split('/').LastOrDefault();
                    if (!string.IsNullOrEmpty(videoId) && int.TryParse(videoId, out _))
                    {
                        return $"https://player.vimeo.com/video/{videoId}";
                    }
                }
                // If already an embed URL, return as is
                else if (videoUrl.Contains("/embed/") || videoUrl.Contains("player.vimeo.com"))
                {
                    return videoUrl;
                }

                // If we can't convert, return original URL
                return videoUrl;
            }
            catch
            {
                // If any error occurs, return original URL
                return videoUrl;
            }
        }

        /// <summary>
        /// Formats duration from TimeSpan to human readable string
        /// </summary>
        public static string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalHours >= 1)
            {
                return $"{(int)duration.TotalHours}:{duration.Minutes:D2}:{duration.Seconds:D2}";
            }
            return $"{duration.Minutes}:{duration.Seconds:D2}";
        }

        /// <summary>
        /// Creates a URL-friendly slug from a string
        /// </summary>
        public static string CreateSlug(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.ToLowerInvariant()
                       .Replace(" ", "-")
                       .Replace("&", "and")
                       .Replace("ñ", "n")
                       .Replace("á", "a")
                       .Replace("é", "e")
                       .Replace("í", "i")
                       .Replace("ó", "o")
                       .Replace("ú", "u")
                       .Replace("ü", "u")
                       .Replace("ç", "c");
        }

        /// <summary>
        /// Formats view count to human readable string (1.2K, 5.4M, etc.)
        /// </summary>
        public static string FormatViewCount(int viewCount)
        {
            if (viewCount >= 1000000)
            {
                return $"{(viewCount / 1000000.0):0.#}M";
            }
            else if (viewCount >= 1000)
            {
                return $"{(viewCount / 1000.0):0.#}K";
            }
            return viewCount.ToString();
        }

        /// <summary>
        /// Generate YouTube thumbnail URL from video ID
        /// </summary>
        public static string GetYoutubeThumbnail(string videoUrl)
        {
            if (string.IsNullOrEmpty(videoUrl))
                return "/img/default-video-thumb.jpg";

            try
            {
                string videoId = "";
                
                if (videoUrl.Contains("youtube.com/watch"))
                {
                    var uri = new Uri(videoUrl);
                    var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                    if (query.TryGetValue("v", out var id))
                    {
                        videoId = id;
                    }
                }
                else if (videoUrl.Contains("youtu.be/"))
                {
                    videoId = videoUrl.Split('/').LastOrDefault() ?? "";
                }
                else if (videoUrl.Contains("/embed/"))
                {
                    videoId = videoUrl.Split('/').LastOrDefault() ?? "";
                }

                if (!string.IsNullOrEmpty(videoId))
                {
                    return $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
                }
            }
            catch
            {
                // If any error occurs, return default thumbnail
            }

            return "/img/default-video-thumb.jpg";
        }
    }
}