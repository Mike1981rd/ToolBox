using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class VideoManagementController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_video_management_list";
            ViewBag.HideTitleInLayout = true;
            return View();
        }

        public IActionResult VideoForm(int? id)
        {
            var model = new VideoViewModel();
            
            if (id.HasValue && id.Value > 0)
            {
                // Edit mode - simulate loading existing video
                ViewBag.IsEditMode = true;
                ViewBag.BreadcrumbActiveKey = "breadcrumb_edit_video";
                model = GetSampleVideoById(id.Value);
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
        public JsonResult GetVideos(string? searchTerm = null, int page = 1, int pageSize = 10)
        {
            try
            {
                var videos = GetSampleVideos();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    videos = videos.Where(v => 
                        v.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        v.AuthorName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        v.TopicName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                var totalRecords = videos.Count;
                var pagedVideos = videos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                return Json(new 
                { 
                    success = true, 
                    data = pagedVideos,
                    totalRecords = totalRecords,
                    page = page,
                    pageSize = pageSize,
                    totalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error loading videos: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult SaveVideo([FromBody] VideoViewModel model)
        {
            try
            {
                // Placeholder logic - in real implementation, save to database
                if (model.Id == 0)
                {
                    // Creating new video
                    model.Id = new Random().Next(1000, 9999);
                    model.UploadDate = DateTime.Now;
                }
                else
                {
                    // Updating existing video
                    model.UpdatedAt = DateTime.Now;
                }

                return Json(new { success = true, message = "Video saved successfully", data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error saving video: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult DeleteVideo(int videoId)
        {
            try
            {
                // Placeholder logic - in real implementation, delete from database
                return Json(new { success = true, message = "Video deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting video: {ex.Message}" });
            }
        }

        [HttpGet]
        public JsonResult GetVideo(int id)
        {
            try
            {
                var video = GetSampleVideoById(id);
                if (video == null)
                {
                    return Json(new { success = false, message = "Video not found" });
                }

                return Json(new { success = true, data = video });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error retrieving video: {ex.Message}" });
            }
        }

        [HttpGet]
        public JsonResult GetTopicsForDropdown()
        {
            try
            {
                var topics = GetSampleTopics();
                return Json(new { success = true, data = topics });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error loading topics: {ex.Message}" });
            }
        }

        [HttpGet]
        public JsonResult GetUsersForDropdown()
        {
            try
            {
                var users = GetSampleUsers();
                return Json(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error loading users: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult UploadVideoFile(IFormFile videoFile)
        {
            try
            {
                if (videoFile == null || videoFile.Length == 0)
                {
                    return Json(new { success = false, message = "No file selected" });
                }

                // Placeholder logic - simulate file upload
                var fileName = $"video_{Guid.NewGuid()}{Path.GetExtension(videoFile.FileName)}";
                var filePath = $"/uploads/videos/{fileName}";

                return Json(new 
                { 
                    success = true, 
                    message = "File uploaded successfully",
                    data = new { fileName = fileName, filePath = filePath, fileSize = videoFile.Length }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error uploading file: {ex.Message}" });
            }
        }

        #region Sample Data Methods

        private List<VideoViewModel> GetSampleVideos()
        {
            return new List<VideoViewModel>
            {
                new VideoViewModel
                {
                    Id = 1,
                    Title = "Introduction to Mindfulness Meditation",
                    Description = "Learn the basics of mindfulness meditation and how to incorporate it into your daily routine.",
                    VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                    AuthorId = 1,
                    AuthorName = "Carlos Checo",
                    TopicId = 1,
                    TopicName = "Mindfulness",
                    MediaType = "youtube",
                    IsFeatured = true,
                    Duration = "15:30",
                    UploadDate = DateTime.Now.AddDays(-10),
                    ViewCount = 250,
                    ThumbnailUrl = "/img/default-video-thumb.jpg"
                },
                new VideoViewModel
                {
                    Id = 2,
                    Title = "Productivity Hacks for Remote Workers",
                    Description = "Discover effective strategies to boost your productivity while working from home.",
                    VideoUrl = "https://vimeo.com/123456789",
                    AuthorId = 2,
                    AuthorName = "Admin User",
                    TopicId = 2,
                    TopicName = "Productivity",
                    MediaType = "vimeo",
                    IsFeatured = false,
                    Duration = "22:45",
                    UploadDate = DateTime.Now.AddDays(-5),
                    ViewCount = 180,
                    ThumbnailUrl = "/img/default-video-thumb.jpg"
                },
                new VideoViewModel
                {
                    Id = 3,
                    Title = "Leadership in Times of Change",
                    Description = "How to lead effectively during challenging and uncertain times.",
                    VideoFilePath = "/uploads/videos/leadership-change.mp4",
                    AuthorId = 3,
                    AuthorName = "Maria Rodriguez",
                    TopicId = 3,
                    TopicName = "Leadership",
                    MediaType = "uploadedfile",
                    IsFeatured = true,
                    Duration = "18:20",
                    UploadDate = DateTime.Now.AddDays(-2),
                    ViewCount = 95,
                    ThumbnailUrl = "/img/default-video-thumb.jpg"
                }
            };
        }

        private VideoViewModel? GetSampleVideoById(int id)
        {
            return GetSampleVideos().FirstOrDefault(v => v.Id == id);
        }

        private List<VideoTopicViewModel> GetSampleTopics()
        {
            return new List<VideoTopicViewModel>
            {
                new VideoTopicViewModel { Id = 1, Name = "Mindfulness" },
                new VideoTopicViewModel { Id = 2, Name = "Productivity" },
                new VideoTopicViewModel { Id = 3, Name = "Leadership" },
                new VideoTopicViewModel { Id = 4, Name = "Communication" }
            };
        }

        private List<VideoAuthorViewModel> GetSampleUsers()
        {
            return new List<VideoAuthorViewModel>
            {
                new VideoAuthorViewModel { Id = 1, Name = "Carlos Checo", Email = "carlos@toolbox.com" },
                new VideoAuthorViewModel { Id = 2, Name = "Admin User", Email = "admin@toolbox.com" },
                new VideoAuthorViewModel { Id = 3, Name = "Maria Rodriguez", Email = "maria@toolbox.com" }
            };
        }

        #endregion
    }
}