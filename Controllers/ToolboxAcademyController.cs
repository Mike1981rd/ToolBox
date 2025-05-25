using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Services;

namespace ToolBox.Controllers
{
    public class ToolboxAcademyController : Controller
    {
        private readonly ILogger<ToolboxAcademyController> _logger;
        private readonly IVideoService _videoService;
        private readonly ApplicationDbContext _context;

        public ToolboxAcademyController(
            ILogger<ToolboxAcademyController> logger,
            IVideoService videoService,
            ApplicationDbContext context)
        {
            _logger = logger;
            _videoService = videoService;
            _context = context;
        }

        public async Task<IActionResult> Index(int? temaId = null, string search = null, int page = 1)
        {
            // Get real data from database
            var temas = await GetTemasWithCountsAsync();
            var allVideos = await GetPublishedVideosAsync(temaId, search);
            
            // Pagination
            var videosPerPage = 6;
            var totalVideos = allVideos.Count;
            var totalPages = (int)Math.Ceiling((double)totalVideos / videosPerPage);
            var videos = allVideos
                .Skip((page - 1) * videosPerPage)
                .Take(videosPerPage)
                .ToList();
            
            // Set active tema
            foreach (var tema in temas)
            {
                tema.IsActive = tema.Id == (temaId ?? 0);
            }
            
            // Calculate statistics
            var stats = await CalculateStatsAsync();
            var featuredVideos = allVideos.Where(v => v.IsFeatured).ToList();

            var model = new ToolboxAcademyIndexViewModel
            {
                Videos = videos,
                Categories = temas,
                CurrentCategorySlug = temaId?.ToString() ?? "all",
                CurrentCategoryName = temas.FirstOrDefault(c => c.Id == temaId)?.Name ?? "Todos los Temas",
                TotalVideos = totalVideos,
                CurrentPage = page,
                TotalPages = totalPages,
                VideosPerPage = videosPerPage,
                HasFeaturedVideos = allVideos.Any(v => v.IsFeatured),
                SearchQuery = search ?? string.Empty,
                
                // Header Statistics
                FeaturedVideosCount = featuredVideos.Count,
                TotalCategoriesCount = temas.Count(c => c.Id != 0),
                TotalViewsCount = 0, // TODO: Implement view tracking
                TotalViewsFormatted = "0",
                TotalWatchTime = TimeSpan.Zero, // TODO: Calculate from duration
                TotalWatchTimeFormatted = "0h",
                
                // Stats for sidebar
                Stats = stats,
                
                // Featured card
                FeaturedCard = new FeaturedCardViewModel
                {
                    Count = featuredVideos.Count,
                    FeaturedVideos = featuredVideos.Take(3).ToList()
                },
                
                // Set header content
                HeaderTitle = "Academia Toolbox",
                HeaderSubtitle = "Explora nuestra biblioteca de conocimiento",
                HeaderDescription = "Videos educativos para impulsar tu crecimiento profesional y personal"
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var video = await GetVideoDetailAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // API Endpoints for Portal
        [HttpGet]
        public async Task<JsonResult> GetPortalVideos(int? temaId = null, string search = null, int page = 1, int pageSize = 6)
        {
            try
            {
                var query = _context.Videos
                    .Include(v => v.Autor)
                    .Include(v => v.Tema)
                    .Where(v => v.EstadoVideo == "Publicado")
                    .AsQueryable();

                // Filter by tema
                if (temaId.HasValue && temaId.Value > 0)
                {
                    query = query.Where(v => v.TemaId == temaId.Value);
                }

                // Search filter
                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();
                    query = query.Where(v => 
                        v.Titulo.ToLower().Contains(search) ||
                        (v.DescripcionHTML != null && v.DescripcionHTML.ToLower().Contains(search)) ||
                        (v.Tema != null && v.Tema.Name.ToLower().Contains(search)) ||
                        (v.PalabrasClaveSEO != null && v.PalabrasClaveSEO.ToLower().Contains(search))
                    );
                }

                // Get total count
                var totalCount = await query.CountAsync();

                // Apply pagination and get data
                var videos = await query
                    .OrderByDescending(v => v.EsDestacado)
                    .ThenByDescending(v => v.FechaSubida)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(v => new
                    {
                        id = v.Id,
                        titulo = v.Titulo,
                        descripcion = v.DescripcionHTML ?? "",
                        thumbnailUrl = GetVideoThumbnail(v),
                        videoUrl = v.TipoFuenteVideo == "Upload" ? v.PathVideoSubido : v.UrlVideoExterno,
                        tipoVideo = v.TipoFuenteVideo,
                        duracion = v.Duracion ?? "00:00",
                        esDestacado = v.EsDestacado,
                        fechaSubida = v.FechaSubida,
                        tema = v.Tema != null ? new { id = v.Tema.Id, nombre = v.Tema.Name } : null,
                        autor = v.Autor != null ? new { id = v.Autor.Id, nombre = v.Autor.FullName } : null
                    })
                    .ToListAsync();

                return Json(new
                {
                    success = true,
                    data = videos,
                    totalCount = totalCount,
                    currentPage = page,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting portal videos");
                return Json(new { success = false, message = "Error al obtener los videos" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTemasConConteo()
        {
            try
            {
                var temas = await _context.Topics
                    .Where(t => t.IsActive)
                    .Select(t => new
                    {
                        id = t.Id,
                        nombre = t.Name,
                        descripcion = t.Description ?? "",
                        conteoVideos = _context.Videos.Count(v => v.TemaId == t.Id && v.EstadoVideo == "Publicado")
                    })
                    .OrderBy(t => t.nombre)
                    .ToListAsync();

                // Add "All" option at the beginning
                var allTemas = new List<object>
                {
                    new
                    {
                        id = 0,
                        nombre = "Todos los Temas",
                        descripcion = "Ver todos los videos disponibles",
                        conteoVideos = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado")
                    }
                };
                allTemas.AddRange(temas);

                return Json(new { success = true, data = allTemas });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting temas with counts");
                return Json(new { success = false, message = "Error al obtener los temas" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetEstadisticasPortal()
        {
            try
            {
                var stats = new
                {
                    totalVideos = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado"),
                    videosDestacados = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado" && v.EsDestacado),
                    totalTemas = await _context.Topics.CountAsync(t => t.IsActive),
                    totalAutores = await _context.Videos
                        .Where(v => v.EstadoVideo == "Publicado" && v.AutorId != null)
                        .Select(v => v.AutorId)
                        .Distinct()
                        .CountAsync()
                };

                return Json(new { success = true, data = stats });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting portal statistics");
                return Json(new { success = false, message = "Error al obtener las estadísticas" });
            }
        }

        // Helper Methods
        private async Task<List<CategoryFilterViewModel>> GetTemasWithCountsAsync()
        {
            var temasData = await _context.Topics
                .Where(t => t.IsActive)
                .Select(t => new 
                {
                    t.Id,
                    t.Name,
                    t.Description,
                    VideoCount = _context.Videos.Count(v => v.TemaId == t.Id && v.EstadoVideo == "Publicado"),
                    FeaturedCount = _context.Videos.Count(v => v.TemaId == t.Id && v.EstadoVideo == "Publicado" && v.EsDestacado)
                })
                .OrderBy(t => t.Name)
                .ToListAsync();

            var temas = temasData.Select((t, index) => new CategoryFilterViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Slug = t.Id.ToString(),
                VideoCount = t.VideoCount,
                IconClass = "fas fa-folder",
                IconEmoji = GetTemaEmoji(index),
                Color = GetTemaColor(index),
                BackgroundColor = "#f8f9fa",
                Description = t.Description ?? "",
                FeaturedVideosCount = t.FeaturedCount
            }).ToList();

            // Add "All" option at the beginning
            var allCount = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado");
            var allFeaturedCount = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado" && v.EsDestacado);
            
            temas.Insert(0, new CategoryFilterViewModel
            {
                Id = 0,
                Name = "Todos los Temas",
                Slug = "all",
                VideoCount = allCount,
                IconClass = "fas fa-th-large",
                IconEmoji = "📚",
                Color = "#6c757d",
                BackgroundColor = "#f8f9fa",
                Description = "Ver todos los videos disponibles",
                FeaturedVideosCount = allFeaturedCount
            });

            return temas;
        }

        private async Task<List<AcademyVideoItemViewModel>> GetPublishedVideosAsync(int? temaId, string search)
        {
            var query = _context.Videos
                .Include(v => v.Autor)
                .Include(v => v.Tema)
                .Where(v => v.EstadoVideo == "Publicado")
                .AsQueryable();

            // Filter by tema
            if (temaId.HasValue && temaId.Value > 0)
            {
                query = query.Where(v => v.TemaId == temaId.Value);
            }

            // Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(v => 
                    v.Titulo.ToLower().Contains(search) ||
                    (v.DescripcionHTML != null && v.DescripcionHTML.ToLower().Contains(search)) ||
                    (v.Tema != null && v.Tema.Name.ToLower().Contains(search)) ||
                    (v.PalabrasClaveSEO != null && v.PalabrasClaveSEO.ToLower().Contains(search))
                );
            }

            var videosData = await query
                .OrderByDescending(v => v.EsDestacado)
                .ThenByDescending(v => v.FechaSubida)
                .ToListAsync();

            var videos = videosData.Select(v => new AcademyVideoItemViewModel
            {
                Id = v.Id,
                Title = v.Titulo,
                ShortDescription = CreateShortDescription(v.DescripcionHTML),
                EmbedUrl = v.TipoFuenteVideo == "Upload" ? v.PathVideoSubido : v.UrlVideoExterno,
                ThumbnailUrl = GetVideoThumbnail(v),
                CategoryId = v.TemaId ?? 0,
                CategoryName = v.Tema?.Name ?? "Sin Tema",
                CategorySlug = v.TemaId?.ToString() ?? "0",
                CategoryIcon = "📁",
                CategoryColor = "#667eea",
                PublishDate = v.FechaSubida,
                PublishDateFormatted = v.FechaSubida.ToString("MMM dd, yyyy"),
                AuthorName = v.Autor?.FullName ?? "Anónimo",
                AuthorAvatar = !string.IsNullOrEmpty(v.Autor?.AvatarUrl) ? v.Autor.AvatarUrl : "/img/default-avatar.png",
                IsFeatured = v.EsDestacado,
                Duration = ParseDuration(v.Duracion),
                DurationFormatted = v.Duracion ?? "00:00",
                ViewCount = 0, // TODO: Implement view tracking
                ViewCountFormatted = "0",
                IsVideoAvailable = true,
                Rating = 0,
                LikesCount = 0,
                Tags = ParseTags(v.PalabrasClaveSEO)
            }).ToList();

            return videos;
        }

        private async Task<AcademyStatsViewModel> CalculateStatsAsync()
        {
            var totalVideos = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado");
            var featuredCount = await _context.Videos.CountAsync(v => v.EstadoVideo == "Publicado" && v.EsDestacado);
            var totalTemas = await _context.Topics.CountAsync(t => t.IsActive);
            var totalAuthors = await _context.Videos
                .Where(v => v.EstadoVideo == "Publicado" && v.AutorId != null)
                .Select(v => v.AutorId)
                .Distinct()
                .CountAsync();

            return new AcademyStatsViewModel
            {
                TotalVideos = totalVideos,
                FeaturedVideos = featuredCount,
                TotalCategories = totalTemas,
                TotalAuthors = totalAuthors,
                TotalViews = 0, // TODO: Implement view tracking
                TotalViewsFormatted = "0",
                TotalDuration = TimeSpan.Zero, // TODO: Calculate from duration strings
                TotalDurationFormatted = "0h",
                LastUpdated = DateTime.Now,
                AverageRating = 0,
                TotalLikes = 0
            };
        }

        private async Task<AcademyVideoDetailViewModel?> GetVideoDetailAsync(int id)
        {
            var video = await _context.Videos
                .Include(v => v.Autor)
                .Include(v => v.Tema)
                .FirstOrDefaultAsync(v => v.Id == id && v.EstadoVideo == "Publicado");

            if (video == null)
                return null;

            // Get related videos from the same tema
            var relatedVideosData = await _context.Videos
                .Include(v => v.Autor)
                .Include(v => v.Tema)
                .Where(v => v.Id != id && v.TemaId == video.TemaId && v.EstadoVideo == "Publicado")
                .OrderByDescending(v => v.FechaSubida)
                .Take(3)
                .ToListAsync();

            var relatedVideos = relatedVideosData.Select(v => new AcademyVideoItemViewModel
            {
                Id = v.Id,
                Title = v.Titulo,
                ShortDescription = CreateShortDescription(v.DescripcionHTML),
                ThumbnailUrl = GetVideoThumbnail(v),
                CategoryName = v.Tema?.Name ?? "Sin Tema",
                PublishDateFormatted = v.FechaSubida.ToString("MMM dd, yyyy"),
                DurationFormatted = v.Duracion ?? "00:00"
            }).ToList();

            return new AcademyVideoDetailViewModel
            {
                Id = video.Id,
                Title = video.Titulo,
                EmbedUrl = video.TipoFuenteVideo == "Upload" ? video.PathVideoSubido : video.UrlVideoExterno,
                FullDescriptionHtml = video.DescripcionHTML ?? "",
                CategoryName = video.Tema?.Name ?? "Sin Tema",
                CategorySlug = video.TemaId?.ToString() ?? "0",
                CategoryId = video.TemaId ?? 0,
                PublishDate = video.FechaSubida,
                PublishDateFormatted = video.FechaSubida.ToString("MMM dd, yyyy"),
                AuthorName = video.Autor?.FullName ?? "Anónimo",
                IsFeatured = video.EsDestacado,
                Duration = ParseDuration(video.Duracion),
                DurationFormatted = video.Duracion ?? "00:00",
                ViewCount = 0, // TODO: Implement view tracking
                Tags = string.IsNullOrEmpty(video.PalabrasClaveSEO) ? new List<string>() : video.PalabrasClaveSEO.Split(',').Select(t => t.Trim()).ToList(),
                RelatedVideos = relatedVideos
            };
        }

        // Utility Methods
        private string GetVideoThumbnail(Video video)
        {
            if (video.TipoFuenteVideo == "YouTube" && !string.IsNullOrEmpty(video.UrlVideoExterno))
            {
                var videoId = ExtractYouTubeVideoId(video.UrlVideoExterno);
                if (!string.IsNullOrEmpty(videoId))
                    return $"https://img.youtube.com/vi/{videoId}/maxresdefault.jpg";
            }
            else if (video.TipoFuenteVideo == "Vimeo" && !string.IsNullOrEmpty(video.UrlVideoExterno))
            {
                // TODO: Implement Vimeo thumbnail extraction
                return "/img/video-placeholder.jpg";
            }
            else if (video.TipoFuenteVideo == "Upload")
            {
                // For uploaded videos, we'll use a custom video thumbnail placeholder
                // In a production environment, you would generate thumbnails from the video
                return "/img/video-upload-placeholder.jpg";
            }
            
            return "/img/video-placeholder.jpg";
        }

        private string ExtractYouTubeVideoId(string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            // Handle different YouTube URL formats
            var patterns = new[]
            {
                @"(?:youtube\.com/watch\?v=|youtu\.be/|youtube\.com/embed/)([\w-]+)",
                @"youtube\.com/watch\?.*v=([\w-]+)"
            };

            foreach (var pattern in patterns)
            {
                var match = System.Text.RegularExpressions.Regex.Match(url, pattern);
                if (match.Success && match.Groups.Count > 1)
                {
                    return match.Groups[1].Value;
                }
            }

            return string.Empty;
        }

        private static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            // Simple HTML stripping
            return System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);
        }

        private TimeSpan ParseDuration(string duration)
        {
            if (string.IsNullOrEmpty(duration))
                return TimeSpan.Zero;

            try
            {
                var parts = duration.Split(':');
                if (parts.Length == 2)
                {
                    return new TimeSpan(0, int.Parse(parts[0]), int.Parse(parts[1]));
                }
                else if (parts.Length == 3)
                {
                    return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                }
            }
            catch { }

            return TimeSpan.Zero;
        }

        private string CreateShortDescription(string html)
        {
            var plainText = StripHtml(html ?? "");
            if (plainText.Length > 200)
            {
                return plainText.Substring(0, 200) + "...";
            }
            return plainText;
        }

        private List<string> ParseTags(string tags)
        {
            if (string.IsNullOrEmpty(tags))
                return new List<string>();

            return tags.Split(',').Select(t => t.Trim()).ToList();
        }

        private string GetTemaEmoji(int index)
        {
            var emojis = new[] { "📚", "🎯", "💡", "🚀", "📊", "🎨", "💼", "🔧", "📱", "🌟" };
            return emojis[index % emojis.Length];
        }

        private string GetTemaColor(int index)
        {
            var colors = new[] { "#667eea", "#4ade80", "#f59e0b", "#ef4444", "#8b5cf6", "#ec4899", "#3b82f6", "#10b981", "#f97316", "#6366f1" };
            return colors[index % colors.Length];
        }
    }
}
