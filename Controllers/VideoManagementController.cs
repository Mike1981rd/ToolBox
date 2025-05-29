using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;
using ToolBox.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ToolBox.Controllers
{
    public class VideoManagementController : BaseController
    {
        private readonly IVideoService _videoService;
        private readonly ITopicService _topicService;
        private readonly IUserService _userService;
        private readonly ILogger<VideoManagementController> _logger;

        public VideoManagementController(
            IVideoService videoService,
            ITopicService topicService,
            IUserService userService,
            ILogger<VideoManagementController> logger)
        {
            _videoService = videoService;
            _topicService = topicService;
            _userService = userService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_video_management_list";
            ViewBag.HideTitleInLayout = true;
            return View();
        }

        public async Task<IActionResult> VideoForm(int? id)
        {
            var model = new VideoCreateEditViewModel();
            
            if (id.HasValue && id.Value > 0)
            {
                // Edit mode
                ViewBag.IsEditMode = true;
                ViewBag.BreadcrumbActiveKey = "breadcrumb_edit_video";
                
                var video = await _videoService.GetByIdAsync(id.Value);
                if (video == null)
                {
                    return NotFound();
                }
                
                model = new VideoCreateEditViewModel
                {
                    Id = video.Id,
                    Titulo = video.Titulo,
                    DescripcionHTML = video.DescripcionHTML,
                    AutorId = video.AutorId,
                    TemaId = video.TemaId,
                    TipoFuenteVideo = video.TipoFuenteVideo,
                    UrlVideoExterno = video.UrlVideoExterno,
                    NombreArchivoVideoSubido = video.NombreArchivoVideoSubido,
                    PathVideoSubido = video.PathVideoSubido,
                    Duracion = video.Duracion,
                    MetaTituloSEO = video.MetaTituloSEO,
                    MetaDescripcionSEO = video.MetaDescripcionSEO,
                    PalabrasClaveSEO = video.PalabrasClaveSEO,
                    EsDestacado = video.EsDestacado,
                    EstadoVideo = video.EstadoVideo
                };
            }
            else
            {
                // Add mode
                ViewBag.IsEditMode = false;
                ViewBag.BreadcrumbActiveKey = "breadcrumb_add_new_video";
            }

            ViewBag.HideTitleInLayout = true;
            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetVideos(string? searchTerm = null, int page = 1, int pageSize = 10, string statusFilter = "all", string? typeFilter = null, string? featuredFilter = null)
        {
            try
            {
                var (videos, totalCount) = await _videoService.GetPaginatedAsync(searchTerm, page, pageSize, statusFilter == "all" ? null : statusFilter, typeFilter, featuredFilter);

                var videoViewModels = videos.Select(v => new VideoListViewModel
                {
                    Id = v.Id,
                    Titulo = v.Titulo,
                    AutorNombre = v.Autor?.FullName,
                    TemaNombre = v.Tema?.Name,
                    TipoFuenteVideo = v.TipoFuenteVideo,
                    Duracion = v.Duracion,
                    EsDestacado = v.EsDestacado,
                    EstadoVideo = v.EstadoVideo,
                    FechaSubida = v.FechaSubida
                }).ToList();

                return Json(new 
                { 
                    success = true, 
                    data = videoViewModels,
                    totalRecords = totalCount,
                    page = page,
                    pageSize = pageSize,
                    totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading videos");
                
                // Check if it's a table doesn't exist error
                if (ex.Message.Contains("does not exist") || ex.Message.Contains("relation") || ex.Message.Contains("table"))
                {
                    return Json(new { success = false, message = "La tabla de videos no existe. Por favor, ejecute las migraciones de la base de datos." });
                }
                
                return Json(new { success = false, message = $"Error al cargar los videos: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveVideo([FromForm] VideoSaveRequest model, IFormFile? videoFile)
        {
            try
            {
                Video video;
                
                if (model.Id == 0)
                {
                    // Creating new video
                    video = new Video
                    {
                        Titulo = model.Titulo,
                        DescripcionHTML = model.DescripcionHTML,
                        AutorId = model.AutorId,
                        TemaId = model.TemaId,
                        TipoFuenteVideo = model.TipoFuenteVideo,
                        UrlVideoExterno = model.UrlVideoExterno,
                        Duracion = model.Duracion,
                        MetaTituloSEO = model.MetaTituloSEO,
                        MetaDescripcionSEO = model.MetaDescripcionSEO,
                        PalabrasClaveSEO = model.PalabrasClaveSEO,
                        EsDestacado = model.EsDestacado,
                        EstadoVideo = model.EstadoVideo,
                        UsuarioCreadorId = GetCurrentUserId()
                    };

                    // Handle video file upload if provided
                    if (videoFile != null && videoFile.Length > 0)
                    {
                        var uploadsPath = Path.Combine("wwwroot", "uploads", "videos");
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(videoFile.FileName)}";
                        var filePath = Path.Combine(uploadsPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await videoFile.CopyToAsync(stream);
                        }

                        video.NombreArchivoVideoSubido = videoFile.FileName;
                        video.PathVideoSubido = $"/uploads/videos/{fileName}";
                    }

                    var created = await _videoService.CreateAsync(video);
                    return Json(new { success = true, message = "Video creado exitosamente", videoId = created.Id, redirectUrl = Url.Action("Index", "VideoManagement") });
                }
                else
                {
                    // Updating existing video
                    var existingVideo = await _videoService.GetByIdAsync(model.Id);
                    if (existingVideo == null)
                    {
                        return Json(new { success = false, message = "Video no encontrado" });
                    }

                    video = new Video
                    {
                        Id = model.Id,
                        Titulo = model.Titulo,
                        DescripcionHTML = model.DescripcionHTML,
                        AutorId = model.AutorId,
                        TemaId = model.TemaId,
                        TipoFuenteVideo = model.TipoFuenteVideo,
                        UrlVideoExterno = model.UrlVideoExterno,
                        Duracion = model.Duracion,
                        MetaTituloSEO = model.MetaTituloSEO,
                        MetaDescripcionSEO = model.MetaDescripcionSEO,
                        PalabrasClaveSEO = model.PalabrasClaveSEO,
                        EsDestacado = model.EsDestacado,
                        EstadoVideo = model.EstadoVideo
                    };

                    // Handle video file upload if provided
                    if (videoFile != null && videoFile.Length > 0)
                    {
                        var uploadsPath = Path.Combine("wwwroot", "uploads", "videos");
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(videoFile.FileName)}";
                        var filePath = Path.Combine(uploadsPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await videoFile.CopyToAsync(stream);
                        }

                        video.NombreArchivoVideoSubido = videoFile.FileName;
                        video.PathVideoSubido = $"/uploads/videos/{fileName}";
                    }
                    else
                    {
                        // Keep existing video info if no new file uploaded
                        video.NombreArchivoVideoSubido = existingVideo.NombreArchivoVideoSubido;
                        video.PathVideoSubido = existingVideo.PathVideoSubido;
                    }

                    await _videoService.UpdateAsync(video);
                    return Json(new { success = true, message = "Video actualizado exitosamente", redirectUrl = Url.Action("Index", "VideoManagement") });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving video");
                return Json(new { success = false, message = "Error al guardar el video" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteVideo([FromBody] DeleteVideoRequest request)
        {
            try
            {
                var deleted = await _videoService.DeleteAsync(request.VideoId);
                if (deleted)
                {
                    return Json(new { success = true, message = "Video eliminado exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Video no encontrado" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting video");
                return Json(new { success = false, message = $"Error al eliminar el video: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetVideo(int id)
        {
            try
            {
                var video = await _videoService.GetByIdAsync(id);
                if (video == null)
                {
                    return Json(new { success = false, message = "Video no encontrado" });
                }

                var viewModel = new VideoCreateEditViewModel
                {
                    Id = video.Id,
                    Titulo = video.Titulo,
                    DescripcionHTML = video.DescripcionHTML,
                    AutorId = video.AutorId,
                    TemaId = video.TemaId,
                    TipoFuenteVideo = video.TipoFuenteVideo,
                    UrlVideoExterno = video.UrlVideoExterno,
                    NombreArchivoVideoSubido = video.NombreArchivoVideoSubido,
                    PathVideoSubido = video.PathVideoSubido,
                    Duracion = video.Duracion,
                    MetaTituloSEO = video.MetaTituloSEO,
                    MetaDescripcionSEO = video.MetaDescripcionSEO,
                    PalabrasClaveSEO = video.PalabrasClaveSEO,
                    EsDestacado = video.EsDestacado,
                    EstadoVideo = video.EstadoVideo
                };

                return Json(new { success = true, data = viewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving video {Id}", id);
                return Json(new { success = false, message = "Error al obtener el video" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTopicsForDropdown()
        {
            try
            {
                var topics = await _topicService.GetAllActiveAsync();
                var topicViewModels = topics
                    .Select(t => new VideoTopicViewModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                        IsActive = t.IsActive
                    })
                    .OrderBy(t => t.Name)
                    .ToList();
                    
                return Json(new { success = true, data = topicViewModels });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading topics for dropdown");
                return Json(new { success = false, message = "Error al cargar los temas" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetUsersForDropdown()
        {
            try
            {
                // Get all active users
                var users = await _userService.GetAllUsersAsync();
                var userViewModels = users
                    .Where(u => u.IsActive)
                    .Select(u => new VideoAuthorViewModel
                    {
                        Id = u.Id,
                        Name = u.FullName,
                        Email = u.Email
                    })
                    .OrderBy(u => u.Name)
                    .ToList();
                    
                return Json(new { success = true, data = userViewModels });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading users for dropdown");
                return Json(new { success = false, message = "Error al cargar los usuarios" });
            }
        }
        
        [HttpPost]
        public async Task<JsonResult> ToggleFeatured([FromBody] ToggleFeaturedRequest request)
        {
            try
            {
                var toggled = await _videoService.ToggleFeaturedAsync(request.VideoId);
                if (toggled)
                {
                    return Json(new { success = true, message = "Estado destacado actualizado" });
                }
                else
                {
                    return Json(new { success = false, message = "Video no encontrado" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling featured status for video");
                return Json(new { success = false, message = "Error al actualizar estado destacado" });
            }
        }
        
        [HttpPost]
        public async Task<JsonResult> UploadVideoFile(IFormFile videoFile)
        {
            try
            {
                if (videoFile == null || videoFile.Length == 0)
                {
                    return Json(new { success = false, message = "No se seleccionó ningún archivo" });
                }

                // Validate file type
                var allowedTypes = new[] { "video/mp4", "video/avi", "video/mov", "video/wmv", "video/webm" };
                if (!allowedTypes.Contains(videoFile.ContentType.ToLower()))
                {
                    return Json(new { success = false, message = "Tipo de archivo no permitido. Use MP4, AVI, MOV, WMV o WebM" });
                }

                // Validate file size (500MB max)
                const long maxSize = 500L * 1024L * 1024L;
                if (videoFile.Length > maxSize)
                {
                    return Json(new { success = false, message = "El archivo es demasiado grande. Máximo 500MB" });
                }

                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine("wwwroot", "uploads", "videos");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(videoFile.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                return Json(new 
                { 
                    success = true, 
                    message = "Archivo subido exitosamente",
                    data = new 
                    { 
                        fileName = videoFile.FileName,
                        savedFileName = fileName,
                        filePath = $"/uploads/videos/{fileName}",
                        fileSize = videoFile.Length
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading video file");
                return Json(new { success = false, message = "Error al subir el archivo" });
            }
        }
        
        [HttpGet]
        public JsonResult TestConnection()
        {
            return Json(new { success = true, message = "Video Management Controller is working", timestamp = DateTime.Now });
        }
        
    }

    public class DeleteVideoRequest
    {
        public int VideoId { get; set; }
    }

    public class ToggleFeaturedRequest
    {
        public int VideoId { get; set; }
    }
}