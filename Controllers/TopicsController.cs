using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class TopicsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_topics_list";
            ViewBag.HideTitleInLayout = true;
            return View();
        }

        [HttpGet]
        public JsonResult GetTopics(string? searchTerm = null)
        {
            // Placeholder data - simulating database results
            var topics = GetSampleTopics();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                topics = topics.Where(t => t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Json(new { success = true, data = topics });
        }

        [HttpPost]
        public JsonResult SaveTopic([FromBody] TopicViewModel model)
        {
            try
            {
                // Placeholder logic - in real implementation, save to database
                if (model.Id == 0)
                {
                    // Creating new topic
                    model.Id = new Random().Next(1000, 9999); // Simulate auto-generated ID
                    model.CreatedAt = DateTime.Now;
                }
                else
                {
                    // Updating existing topic
                    model.UpdatedAt = DateTime.Now;
                }

                return Json(new { success = true, message = "Topic saved successfully", data = model });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error saving topic: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult DeleteTopic(int topicId)
        {
            try
            {
                // Placeholder logic - in real implementation, delete from database
                return Json(new { success = true, message = "Topic deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error deleting topic: {ex.Message}" });
            }
        }

        [HttpGet]
        public JsonResult GetTopic(int id)
        {
            try
            {
                // Placeholder logic - in real implementation, get from database
                var topic = GetSampleTopics().FirstOrDefault(t => t.Id == id);
                if (topic == null)
                {
                    return Json(new { success = false, message = "Topic not found" });
                }

                return Json(new { success = true, data = topic });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error retrieving topic: {ex.Message}" });
            }
        }

        private List<TopicViewModel> GetSampleTopics()
        {
            return new List<TopicViewModel>
            {
                new TopicViewModel
                {
                    Id = 1,
                    Name = "Mindfulness",
                    Description = "Techniques and practices for mindful living and meditation",
                    Status = "active",
                    IconClass = "fas fa-leaf",
                    IconBackground = "bg-label-success",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },
                new TopicViewModel
                {
                    Id = 2,
                    Name = "Productivity",
                    Description = "Methods and tools to improve personal and professional productivity",
                    Status = "active",
                    IconClass = "fas fa-chart-line",
                    IconBackground = "bg-label-primary",
                    CreatedAt = DateTime.Now.AddDays(-8)
                },
                new TopicViewModel
                {
                    Id = 3,
                    Name = "Leadership",
                    Description = "Leadership skills and management techniques for effective guidance",
                    Status = "active",
                    IconClass = "fas fa-users-cog",
                    IconBackground = "bg-label-warning",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new TopicViewModel
                {
                    Id = 4,
                    Name = "Communication",
                    Description = "Effective communication strategies for personal and professional relationships",
                    Status = "inactive",
                    IconClass = "fas fa-comments",
                    IconBackground = "bg-label-info",
                    CreatedAt = DateTime.Now.AddDays(-3)
                }
            };
        }
    }
}