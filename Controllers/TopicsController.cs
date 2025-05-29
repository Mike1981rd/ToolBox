using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;
using ToolBox.Interfaces;

namespace ToolBox.Controllers
{
    public class TopicsController : BaseController
    {
        private readonly ITopicService _topicService;
        private readonly ILogger<TopicsController> _logger;

        public TopicsController(ITopicService topicService, ILogger<TopicsController> logger)
        {
            _topicService = topicService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.BreadcrumbActiveKey = "breadcrumb_topics_list";
            ViewBag.HideTitleInLayout = true;
            return View();
        }

        // API Endpoints
        [HttpGet]
        [Route("api/academia/temas")]
        public async Task<IActionResult> GetTopicsApi(
            string? searchTerm = null, 
            int page = 1, 
            int pageSize = 10,
            bool includeInactive = false)
        {
            try
            {
                var (topics, totalCount) = await _topicService.GetPaginatedAsync(
                    searchTerm, page, pageSize, includeInactive);

                var topicViewModels = topics.Select(t => new TopicListViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    IsActive = t.IsActive,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    CreatedByUserName = t.CreatedByUser?.FullName
                }).ToList();

                var response = new TopicPaginatedResponseViewModel
                {
                    Topics = topicViewModels,
                    TotalCount = totalCount,
                    CurrentPage = page,
                    PageSize = pageSize
                };

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topics");
                return StatusCode(500, new { success = false, message = "Error al obtener los temas" });
            }
        }

        [HttpGet]
        [Route("api/academia/temas/{id}")]
        public async Task<IActionResult> GetTopicApi(int id)
        {
            try
            {
                var topic = await _topicService.GetByIdAsync(id);
                if (topic == null)
                {
                    return NotFound(new { success = false, message = "Tema no encontrado" });
                }

                var viewModel = new TopicCreateEditViewModel
                {
                    Id = topic.Id,
                    Name = topic.Name,
                    Description = topic.Description,
                    IsActive = topic.IsActive
                };

                return Ok(new { success = true, data = viewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topic {Id}", id);
                return StatusCode(500, new { success = false, message = "Error al obtener el tema" });
            }
        }

        [HttpPost]
        [Route("api/academia/temas")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTopic([FromBody] TopicCreateEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { 
                        success = false, 
                        message = "Datos inválidos",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var topic = new Topic
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    CreatedByUserId = GetCurrentUserId()
                };

                var createdTopic = await _topicService.CreateAsync(topic);

                return Ok(new { 
                    success = true, 
                    message = "Tema creado exitosamente",
                    data = new { id = createdTopic.Id }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating topic");
                return StatusCode(500, new { success = false, message = "Error al crear el tema" });
            }
        }

        [HttpPut]
        [Route("api/academia/temas/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicCreateEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { 
                        success = false, 
                        message = "Datos inválidos",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var topic = new Topic
                {
                    Id = id,
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive
                };

                var updated = await _topicService.UpdateAsync(topic);
                
                if (!updated)
                {
                    return NotFound(new { success = false, message = "Tema no encontrado" });
                }

                return Ok(new { success = true, message = "Tema actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating topic {Id}", id);
                return StatusCode(500, new { success = false, message = "Error al actualizar el tema" });
            }
        }

        [HttpDelete]
        [Route("api/academia/temas/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTopicApi(int id)
        {
            try
            {
                var deleted = await _topicService.DeleteAsync(id);
                
                if (!deleted)
                {
                    return NotFound(new { success = false, message = "Tema no encontrado" });
                }

                return Ok(new { success = true, message = "Tema eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting topic {Id}", id);
                return StatusCode(500, new { success = false, message = "Error al eliminar el tema" });
            }
        }

        [HttpPost]
        [Route("api/academia/temas/{id}/toggle-status")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                var (success, message, newState) = await _topicService.ToggleStatusAsync(id);
                
                if (!success)
                {
                    return NotFound(new { success = false, message });
                }

                return Ok(new { success = true, message, newIsActiveState = newState });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling topic status {Id}", id);
                return StatusCode(500, new { success = false, message = "Error al cambiar el estado del tema" });
            }
        }

        // Legacy endpoints for compatibility
        [HttpGet]
        public async Task<JsonResult> GetTopics(
            string? searchTerm = null,
            int page = 1,
            int pageSize = 10,
            string statusFilter = "all")
        {
            try
            {
                // For now, get all topics and filter in memory until TopicService is updated
                var includeInactive = statusFilter != "active";
                
                var (allTopics, _) = await _topicService.GetPaginatedAsync(
                    searchTerm, 
                    1, 
                    1000, // Get all for now
                    includeInactive: includeInactive);

                // Apply status filter
                var filteredTopics = statusFilter switch
                {
                    "active" => allTopics.Where(t => t.IsActive).ToList(),
                    "inactive" => allTopics.Where(t => !t.IsActive).ToList(),
                    _ => allTopics.ToList() // "all"
                };

                // Calculate pagination
                var totalCount = filteredTopics.Count();
                var topics = filteredTopics
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var topicViewModels = topics.Select(t => new
                {
                    id = t.Id,
                    name = t.Name,
                    description = t.Description,
                    isActive = t.IsActive,
                    status = t.IsActive ? "active" : "inactive",
                    createdAt = t.CreatedAt,
                    updatedAt = t.UpdatedAt
                }).ToList();

                return Json(new 
                { 
                    success = true, 
                    topics = topicViewModels,
                    totalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting topics (legacy)");
                return Json(new { success = false, message = "Error al obtener los temas" });
            }
        }

        public class SaveTopicRequest
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public bool IsActive { get; set; } = true;
        }

        [HttpPost]
        public async Task<JsonResult> SaveTopic([FromBody] SaveTopicRequest model)
        {
            try
            {
                if (model.Id == 0)
                {
                    // Create
                    var topic = new Topic
                    {
                        Name = model.Name,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        CreatedByUserId = GetCurrentUserId()
                    };

                    var created = await _topicService.CreateAsync(topic);
                    
                    return Json(new { 
                        success = true, 
                        message = "Tema creado exitosamente", 
                        data = new {
                            id = created.Id,
                            name = created.Name,
                            description = created.Description,
                            isActive = created.IsActive,
                            createdAt = created.CreatedAt
                        }
                    });
                }
                else
                {
                    // Update
                    var topic = new Topic
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Description = model.Description,
                        IsActive = model.IsActive
                    };

                    await _topicService.UpdateAsync(topic);
                    
                    return Json(new { 
                        success = true, 
                        message = "Tema actualizado exitosamente"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving topic (legacy)");
                return Json(new { success = false, message = $"Error al guardar el tema: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTopic([FromBody] DeleteTopicRequest request)
        {
            try
            {
                var deleted = await _topicService.DeleteAsync(request.TopicId);
                
                if (!deleted)
                {
                    return Json(new { success = false, message = "Tema no encontrado" });
                }

                return Json(new { success = true, message = "Tema eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting topic");
                return Json(new { success = false, message = $"Error al eliminar el tema: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTopic(int id)
        {
            try
            {
                var topic = await _topicService.GetByIdAsync(id);
                if (topic == null)
                {
                    return Json(new { success = false, message = "Tema no encontrado" });
                }

                var viewModel = new TopicViewModel
                {
                    Id = topic.Id,
                    Name = topic.Name,
                    Description = topic.Description,
                    Status = topic.IsActive ? "active" : "inactive",
                    CreatedAt = topic.CreatedAt,
                    UpdatedAt = topic.UpdatedAt
                };

                return Json(new { success = true, data = viewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving topic");
                return Json(new { success = false, message = $"Error al obtener el tema: {ex.Message}" });
            }
        }

    }

    public class DeleteTopicRequest
    {
        public int TopicId { get; set; }
    }
}