using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class ToolboxAcademyController : Controller
    {
        private readonly ILogger<ToolboxAcademyController> _logger;

        public ToolboxAcademyController(ILogger<ToolboxAcademyController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string categorySlug = null, string search = null, int page = 1)
        {
            var categories = GetCategories();
            var allVideos = GetSampleVideos();
            
            // Filter by category if specified
            if (!string.IsNullOrEmpty(categorySlug) && categorySlug != "all")
            {
                allVideos = allVideos.Where(v => v.CategorySlug == categorySlug).ToList();
            }
            
            // Filter by search query if specified
            if (!string.IsNullOrEmpty(search))
            {
                allVideos = allVideos.Where(v => 
                    v.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    v.ShortDescription.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    v.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }
            
            // Pagination
            var videosPerPage = 6;
            var totalVideos = allVideos.Count;
            var totalPages = (int)Math.Ceiling((double)totalVideos / videosPerPage);
            var videos = allVideos
                .Skip((page - 1) * videosPerPage)
                .Take(videosPerPage)
                .ToList();
            
            // Set active category
            foreach (var category in categories)
            {
                category.IsActive = category.Slug == categorySlug;
            }
            
            // Calculate statistics
            var stats = CalculateStats(allVideos);
            var featuredVideos = allVideos.Where(v => v.IsFeatured).ToList();

            var model = new ToolboxAcademyIndexViewModel
            {
                Videos = videos,
                Categories = categories,
                CurrentCategorySlug = categorySlug ?? "all",
                CurrentCategoryName = categories.FirstOrDefault(c => c.Slug == categorySlug)?.Name ?? "All Categories",
                TotalVideos = totalVideos,
                CurrentPage = page,
                TotalPages = totalPages,
                VideosPerPage = videosPerPage,
                HasFeaturedVideos = allVideos.Any(v => v.IsFeatured),
                SearchQuery = search ?? string.Empty,
                
                // Header Statistics
                FeaturedVideosCount = featuredVideos.Count,
                TotalCategoriesCount = categories.Count(c => c.Slug != "all"),
                TotalViewsCount = allVideos.Sum(v => v.ViewCount),
                TotalViewsFormatted = VideoUrlHelper.FormatViewCount(allVideos.Sum(v => v.ViewCount)),
                TotalWatchTime = TimeSpan.FromMinutes(allVideos.Sum(v => v.Duration.TotalMinutes)),
                TotalWatchTimeFormatted = VideoUrlHelper.FormatDuration(TimeSpan.FromMinutes(allVideos.Sum(v => v.Duration.TotalMinutes))),
                
                // Stats for sidebar
                Stats = stats,
                
                // Featured card
                FeaturedCard = new FeaturedCardViewModel
                {
                    Count = featuredVideos.Count,
                    FeaturedVideos = featuredVideos.Take(3).ToList()
                }
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var video = GetVideoDetail(id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpGet]
        public JsonResult GetVideos(string categorySlug = null, string search = null, int page = 1)
        {
            try
            {
                var allVideos = GetSampleVideos();
                
                // Filter by category if specified
                if (!string.IsNullOrEmpty(categorySlug) && categorySlug != "all")
                {
                    allVideos = allVideos.Where(v => v.CategorySlug == categorySlug).ToList();
                }
                
                // Filter by search query if specified
                if (!string.IsNullOrEmpty(search))
                {
                    allVideos = allVideos.Where(v => 
                        v.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        v.ShortDescription.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        v.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }
                
                // Pagination
                var videosPerPage = 6;
                var totalVideos = allVideos.Count;
                var totalPages = (int)Math.Ceiling((double)totalVideos / videosPerPage);
                var videos = allVideos
                    .Skip((page - 1) * videosPerPage)
                    .Take(videosPerPage)
                    .ToList();

                return Json(new
                {
                    success = true,
                    videos = videos,
                    totalVideos = totalVideos,
                    currentPage = page,
                    totalPages = totalPages,
                    hasMore = page < totalPages
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetVideoStats()
        {
            try
            {
                var videos = GetSampleVideos();
                var categories = GetCategories();
                
                var stats = new VideoStatsViewModel
                {
                    TotalVideos = videos.Count,
                    TotalCategories = categories.Count,
                    TotalViews = videos.Sum(v => v.ViewCount),
                    TotalDuration = TimeSpan.FromMinutes(videos.Sum(v => v.Duration.TotalMinutes)),
                    FeaturedVideos = videos.Count(v => v.IsFeatured),
                    LastVideoPublished = videos.Max(v => v.PublishDate)
                };
                
                stats.TotalDurationFormatted = VideoUrlHelper.FormatDuration(stats.TotalDuration);

                return Json(new { success = true, stats = stats });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private List<CategoryFilterViewModel> GetCategories()
        {
            var videos = GetSampleVideos();
            
            return new List<CategoryFilterViewModel>
            {
                new CategoryFilterViewModel 
                { 
                    Id = 0, 
                    Name = "All Categories", 
                    Slug = "all", 
                    VideoCount = videos.Count, 
                    IconClass = "fas fa-th-large",
                    IconEmoji = "📁",
                    Color = "#6c757d",
                    BackgroundColor = "#f8f9fa",
                    Description = "Ver todos los videos disponibles"
                },
                new CategoryFilterViewModel 
                { 
                    Id = 1, 
                    Name = "Web Development", 
                    Slug = "web-development", 
                    VideoCount = videos.Count(v => v.CategorySlug == "web-development"), 
                    IconClass = "fas fa-code",
                    IconEmoji = "💻",
                    Color = "#007bff",
                    BackgroundColor = "#e3f2fd",
                    Description = "Desarrollo web y programación",
                    FeaturedVideosCount = videos.Count(v => v.CategorySlug == "web-development" && v.IsFeatured)
                },
                new CategoryFilterViewModel 
                { 
                    Id = 2, 
                    Name = "Project Management", 
                    Slug = "project-management", 
                    VideoCount = videos.Count(v => v.CategorySlug == "project-management"), 
                    IconClass = "fas fa-tasks",
                    IconEmoji = "📊",
                    Color = "#28a745",
                    BackgroundColor = "#e8f5e8",
                    Description = "Gestión de proyectos y equipos",
                    FeaturedVideosCount = videos.Count(v => v.CategorySlug == "project-management" && v.IsFeatured)
                },
                new CategoryFilterViewModel 
                { 
                    Id = 3, 
                    Name = "Design & UI/UX", 
                    Slug = "design-ui-ux", 
                    VideoCount = videos.Count(v => v.CategorySlug == "design-ui-ux"), 
                    IconClass = "fas fa-palette",
                    IconEmoji = "🎨",
                    Color = "#e83e8c",
                    BackgroundColor = "#fce4ec",
                    Description = "Diseño y experiencia de usuario",
                    FeaturedVideosCount = videos.Count(v => v.CategorySlug == "design-ui-ux" && v.IsFeatured)
                },
                new CategoryFilterViewModel 
                { 
                    Id = 4, 
                    Name = "Marketing & Growth", 
                    Slug = "marketing-growth", 
                    VideoCount = videos.Count(v => v.CategorySlug == "marketing-growth"), 
                    IconClass = "fas fa-chart-line",
                    IconEmoji = "📈",
                    Color = "#fd7e14",
                    BackgroundColor = "#fff3e0",
                    Description = "Marketing digital y crecimiento",
                    FeaturedVideosCount = videos.Count(v => v.CategorySlug == "marketing-growth" && v.IsFeatured)
                },
                new CategoryFilterViewModel 
                { 
                    Id = 5, 
                    Name = "Business Strategy", 
                    Slug = "business-strategy", 
                    VideoCount = videos.Count(v => v.CategorySlug == "business-strategy"), 
                    IconClass = "fas fa-briefcase",
                    IconEmoji = "💼",
                    Color = "#6f42c1",
                    BackgroundColor = "#f3e5f5",
                    Description = "Estrategia empresarial",
                    FeaturedVideosCount = videos.Count(v => v.CategorySlug == "business-strategy" && v.IsFeatured)
                }
            };
        }

        private AcademyStatsViewModel CalculateStats(List<AcademyVideoItemViewModel> videos)
        {
            var totalViews = videos.Sum(v => v.ViewCount);
            var totalDuration = TimeSpan.FromMinutes(videos.Sum(v => v.Duration.TotalMinutes));
            var featuredCount = videos.Count(v => v.IsFeatured);
            var authors = videos.Select(v => v.AuthorName).Distinct().Count();

            return new AcademyStatsViewModel
            {
                TotalVideos = videos.Count,
                FeaturedVideos = featuredCount,
                TotalCategories = GetCategories().Count(c => c.Slug != "all"),
                TotalAuthors = authors,
                TotalViews = totalViews,
                TotalViewsFormatted = VideoUrlHelper.FormatViewCount(totalViews),
                TotalDuration = totalDuration,
                TotalDurationFormatted = VideoUrlHelper.FormatDuration(totalDuration),
                LastUpdated = DateTime.Now,
                AverageRating = videos.Average(v => v.Rating),
                TotalLikes = videos.Sum(v => v.LikesCount)
            };
        }

        private List<AcademyVideoItemViewModel> GetSampleVideos()
        {
            var videos = new List<AcademyVideoItemViewModel>
            {
                new AcademyVideoItemViewModel
                {
                    Id = 1,
                    Title = "Getting Started with React Hooks",
                    EmbedUrl = "https://www.youtube.com/embed/O6P86uwfdR0",
                    ThumbnailUrl = VideoUrlHelper.GetYoutubeThumbnail("https://www.youtube.com/embed/O6P86uwfdR0"),
                    ShortDescription = "Learn the fundamentals of React Hooks and how they can simplify your component state management. This comprehensive tutorial covers useState, useEffect, and custom hooks with practical examples.",
                    CategoryName = "Web Development",
                    CategorySlug = "web-development",
                    CategoryId = 1,
                    CategoryIcon = "💻",
                    CategoryColor = "#007bff",
                    PublishDate = DateTime.Now.AddDays(-7),
                    PublishDateFormatted = DateTime.Now.AddDays(-7).ToString("MMM dd, yyyy"),
                    AuthorName = "Sarah Johnson",
                    AuthorAvatar = "/img/authors/sarah-johnson.jpg",
                    IsFeatured = true,
                    Duration = TimeSpan.FromMinutes(24),
                    DurationFormatted = "24:00",
                    ViewCount = 1250,
                    ViewCountFormatted = VideoUrlHelper.FormatViewCount(1250),
                    IsVideoAvailable = true,
                    Rating = 4.8,
                    LikesCount = 95,
                    Tags = new List<string> { "React", "JavaScript", "Frontend", "Hooks", "Tutorial" }
                },
                new AcademyVideoItemViewModel
                {
                    Id = 2,
                    Title = "Agile Project Management Best Practices",
                    EmbedUrl = "https://www.youtube.com/embed/502ILHjX9EE",
                    ThumbnailUrl = VideoUrlHelper.GetYoutubeThumbnail("https://www.youtube.com/embed/502ILHjX9EE"),
                    ShortDescription = "Discover proven strategies for managing agile projects effectively. Learn about sprint planning, daily standups, retrospectives, and how to keep your team productive and focused.",
                    CategoryName = "Project Management",
                    CategorySlug = "project-management",
                    CategoryId = 2,
                    CategoryIcon = "📊",
                    CategoryColor = "#28a745",
                    PublishDate = DateTime.Now.AddDays(-14),
                    PublishDateFormatted = DateTime.Now.AddDays(-14).ToString("MMM dd, yyyy"),
                    AuthorName = "Mike Chen",
                    AuthorAvatar = "/img/authors/mike-chen.jpg",
                    IsFeatured = false,
                    Duration = TimeSpan.FromMinutes(18),
                    DurationFormatted = "18:00",
                    ViewCount = 890,
                    ViewCountFormatted = VideoUrlHelper.FormatViewCount(890),
                    IsVideoAvailable = true,
                    Rating = 4.6,
                    LikesCount = 67,
                    Tags = new List<string> { "Agile", "Project Management", "Scrum", "Leadership", "Team Management" }
                },
                new AcademyVideoItemViewModel
                {
                    Id = 3,
                    Title = "Modern UI Design Principles",
                    EmbedUrl = "https://www.youtube.com/embed/YiLUYf4HDh4",
                    ShortDescription = "Explore contemporary design principles that create intuitive and beautiful user interfaces. Learn about typography, color theory, spacing, and accessibility considerations.",
                    CategoryName = "Design & UI/UX",
                    CategorySlug = "design-ui-ux",
                    CategoryId = 3,
                    PublishDate = DateTime.Now.AddDays(-21),
                    PublishDateFormatted = DateTime.Now.AddDays(-21).ToString("MMM dd, yyyy"),
                    AuthorName = "Emma Rodriguez",
                    IsFeatured = true,
                    Duration = TimeSpan.FromMinutes(32),
                    DurationFormatted = "32:00",
                    ViewCount = 2100
                },
                new AcademyVideoItemViewModel
                {
                    Id = 4,
                    Title = "Digital Marketing Strategies for 2024",
                    EmbedUrl = "https://www.youtube.com/embed/HVyyi9WTf8E",
                    ShortDescription = "Stay ahead of the curve with the latest digital marketing trends and strategies. Learn about social media marketing, content creation, SEO optimization, and conversion tracking.",
                    CategoryName = "Marketing & Growth",
                    CategorySlug = "marketing-growth",
                    CategoryId = 4,
                    PublishDate = DateTime.Now.AddDays(-28),
                    PublishDateFormatted = DateTime.Now.AddDays(-28).ToString("MMM dd, yyyy"),
                    AuthorName = "David Thompson",
                    IsFeatured = false,
                    Duration = TimeSpan.FromMinutes(45),
                    DurationFormatted = "45:00",
                    ViewCount = 1850
                },
                new AcademyVideoItemViewModel
                {
                    Id = 5,
                    Title = "Building Scalable Business Models",
                    EmbedUrl = "https://www.youtube.com/embed/RYdKFESDyXM",
                    ShortDescription = "Learn how to create business models that can scale efficiently. Understand revenue streams, cost structures, and key partnerships that drive sustainable growth.",
                    CategoryName = "Business Strategy",
                    CategorySlug = "business-strategy",
                    CategoryId = 5,
                    PublishDate = DateTime.Now.AddDays(-35),
                    PublishDateFormatted = DateTime.Now.AddDays(-35).ToString("MMM dd, yyyy"),
                    AuthorName = "Lisa Wang",
                    IsFeatured = true,
                    Duration = TimeSpan.FromMinutes(38),
                    DurationFormatted = "38:00",
                    ViewCount = 1650
                },
                new AcademyVideoItemViewModel
                {
                    Id = 6,
                    Title = "Advanced CSS Grid Layouts",
                    EmbedUrl = "https://www.youtube.com/embed/tFKrK4eAiUQ",
                    ShortDescription = "Master CSS Grid to create complex, responsive layouts with ease. Learn grid templates, areas, alignment, and how to build modern web layouts that work across all devices.",
                    CategoryName = "Web Development",
                    CategorySlug = "web-development",
                    CategoryId = 1,
                    PublishDate = DateTime.Now.AddDays(-42),
                    PublishDateFormatted = DateTime.Now.AddDays(-42).ToString("MMM dd, yyyy"),
                    AuthorName = "Alex Kumar",
                    IsFeatured = false,
                    Duration = TimeSpan.FromMinutes(28),
                    DurationFormatted = "28:00",
                    ViewCount = 1420
                },
                new AcademyVideoItemViewModel
                {
                    Id = 7,
                    Title = "User Research Methods That Work",
                    EmbedUrl = "https://www.youtube.com/embed/Ovj4hFxko7c",
                    ShortDescription = "Discover effective user research techniques to understand your audience better. Learn about surveys, interviews, usability testing, and how to translate insights into design decisions.",
                    CategoryName = "Design & UI/UX",
                    CategorySlug = "design-ui-ux",
                    CategoryId = 3,
                    PublishDate = DateTime.Now.AddDays(-49),
                    PublishDateFormatted = DateTime.Now.AddDays(-49).ToString("MMM dd, yyyy"),
                    AuthorName = "Rachel Green",
                    IsFeatured = false,
                    Duration = TimeSpan.FromMinutes(26),
                    DurationFormatted = "26:00",
                    ViewCount = 980
                },
                new AcademyVideoItemViewModel
                {
                    Id = 8,
                    Title = "Remote Team Leadership",
                    EmbedUrl = "https://www.youtube.com/embed/Jjz2z2U6JyU",
                    ShortDescription = "Learn how to effectively lead and manage remote teams in today's distributed work environment. Covers communication strategies, team building, and productivity tools.",
                    CategoryName = "Project Management",
                    CategorySlug = "project-management",
                    CategoryId = 2,
                    PublishDate = DateTime.Now.AddDays(-56),
                    PublishDateFormatted = DateTime.Now.AddDays(-56).ToString("MMM dd, yyyy"),
                    AuthorName = "James Wilson",
                    IsFeatured = false,
                    Duration = TimeSpan.FromMinutes(35),
                    DurationFormatted = "35:00",
                    ViewCount = 1320
                }
            };
            
            // Add missing properties to all videos
            foreach (var video in videos)
            {
                if (string.IsNullOrEmpty(video.ThumbnailUrl))
                    video.ThumbnailUrl = VideoUrlHelper.GetYoutubeThumbnail(video.EmbedUrl);
                if (string.IsNullOrEmpty(video.ViewCountFormatted))
                    video.ViewCountFormatted = VideoUrlHelper.FormatViewCount(video.ViewCount);
                if (string.IsNullOrEmpty(video.AuthorAvatar))
                    video.AuthorAvatar = "/img/default-avatar.png";
                if (video.Rating == 0)
                    video.Rating = 4.0 + (new Random().NextDouble() * 1.0);
                if (video.LikesCount == 0)
                    video.LikesCount = video.ViewCount / 15;
                if (!video.Tags.Any())
                    video.Tags = new List<string> { video.CategoryName.Replace(" ", ""), "Tutorial", "Education" };
                
                // Set category properties
                switch (video.CategorySlug)
                {
                    case "web-development":
                        video.CategoryIcon = "💻";
                        video.CategoryColor = "#007bff";
                        break;
                    case "project-management":
                        video.CategoryIcon = "📊";
                        video.CategoryColor = "#28a745";
                        break;
                    case "design-ui-ux":
                        video.CategoryIcon = "🎨";
                        video.CategoryColor = "#e83e8c";
                        break;
                    case "marketing-growth":
                        video.CategoryIcon = "📈";
                        video.CategoryColor = "#fd7e14";
                        break;
                    case "business-strategy":
                        video.CategoryIcon = "💼";
                        video.CategoryColor = "#6f42c1";
                        break;
                }
            }

            return videos;
        }

        private AcademyVideoDetailViewModel? GetVideoDetail(int id)
        {
            var videos = GetSampleVideos();
            var video = videos.FirstOrDefault(v => v.Id == id);
            
            if (video == null)
                return null;

            // Get related videos from the same category
            var relatedVideos = videos
                .Where(v => v.Id != id && v.CategorySlug == video.CategorySlug)
                .Take(3)
                .ToList();

            return new AcademyVideoDetailViewModel
            {
                Id = video.Id,
                Title = video.Title,
                EmbedUrl = video.EmbedUrl,
                FullDescriptionHtml = GetFullDescription(video.Id),
                CategoryName = video.CategoryName,
                CategorySlug = video.CategorySlug,
                CategoryId = video.CategoryId,
                PublishDate = video.PublishDate,
                PublishDateFormatted = video.PublishDateFormatted,
                AuthorName = video.AuthorName,
                IsFeatured = video.IsFeatured,
                Duration = video.Duration,
                DurationFormatted = video.DurationFormatted,
                ViewCount = video.ViewCount,
                Tags = GetVideoTags(video.Id),
                RelatedVideos = relatedVideos
            };
        }

        private string GetFullDescription(int videoId)
        {
            // Sample full descriptions for different videos
            var descriptions = new Dictionary<int, string>
            {
                [1] = @"<p>React Hooks revolutionized the way we write React components by allowing us to use state and other React features without writing a class component. In this comprehensive tutorial, we'll dive deep into the most commonly used hooks and learn how to leverage them effectively.</p>
                       <h4>What You'll Learn:</h4>
                       <ul>
                         <li>Understanding the useState hook for managing component state</li>
                         <li>Using useEffect for side effects and lifecycle methods</li>
                         <li>Creating custom hooks for reusable logic</li>
                         <li>Best practices and common pitfalls to avoid</li>
                         <li>Performance optimization techniques with useCallback and useMemo</li>
                       </ul>
                       <p>By the end of this video, you'll have a solid understanding of React Hooks and be able to refactor class components into functional components with confidence.</p>",
                
                [2] = @"<p>Agile methodology has become the standard for modern software development and project management. This video covers essential practices that will help you implement agile effectively in your organization.</p>
                       <h4>Key Topics Covered:</h4>
                       <ul>
                         <li>Sprint planning and estimation techniques</li>
                         <li>Conducting effective daily standups</li>
                         <li>Sprint reviews and retrospectives</li>
                         <li>Managing backlog and user stories</li>
                         <li>Tools and techniques for remote agile teams</li>
                       </ul>
                       <p>Whether you're new to agile or looking to improve your existing processes, this video provides practical insights you can implement immediately.</p>",
                
                [3] = @"<p>Modern UI design goes beyond making things look pretty. It's about creating interfaces that are intuitive, accessible, and delightful to use. This video explores the fundamental principles that guide successful UI design.</p>
                       <h4>Design Principles Covered:</h4>
                       <ul>
                         <li>Visual hierarchy and information architecture</li>
                         <li>Typography selection and pairing</li>
                         <li>Color theory and accessibility considerations</li>
                         <li>Spacing, rhythm, and layout systems</li>
                         <li>Micro-interactions and animation principles</li>
                       </ul>
                       <p>Learn how to apply these principles to create designs that not only look great but also provide exceptional user experiences.</p>"
            };

            return descriptions.TryGetValue(videoId, out var description) 
                ? description 
                : "<p>This is a comprehensive tutorial that covers all the essential concepts and practical applications. You'll gain valuable insights and hands-on experience that you can apply immediately in your projects.</p>";
        }

        private List<string> GetVideoTags(int videoId)
        {
            var tags = new Dictionary<int, List<string>>
            {
                [1] = new List<string> { "React", "JavaScript", "Frontend", "Hooks", "Tutorial" },
                [2] = new List<string> { "Agile", "Project Management", "Scrum", "Leadership", "Team Management" },
                [3] = new List<string> { "UI Design", "UX", "Design Principles", "Typography", "Color Theory" },
                [4] = new List<string> { "Digital Marketing", "SEO", "Social Media", "Content Marketing", "Analytics" },
                [5] = new List<string> { "Business Strategy", "Entrepreneurship", "Scaling", "Revenue Models", "Growth" },
                [6] = new List<string> { "CSS", "Web Development", "Layout", "Responsive Design", "Frontend" },
                [7] = new List<string> { "User Research", "UX Design", "Usability Testing", "User Interviews", "Design Research" },
                [8] = new List<string> { "Remote Work", "Team Leadership", "Management", "Communication", "Productivity" }
            };

            return tags.TryGetValue(videoId, out var videoTags) ? videoTags : new List<string>();
        }
    }
}