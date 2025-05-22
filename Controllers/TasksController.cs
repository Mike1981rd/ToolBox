using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class TasksController : Controller
    {
        private static List<TaskViewModel> _mockTasks = new List<TaskViewModel>();
        private static int _nextTaskId = 1;

        public async Task<IActionResult> Index()
        {
            // Initialize with some mock data if empty
            if (!_mockTasks.Any())
            {
                InitializeMockData();
            }

            var statistics = CalculateStatistics(_mockTasks);
            
            var viewModel = new TasksPageViewModel
            {
                Tasks = _mockTasks.OrderByDescending(t => t.CreatedAt).ToList(),
                Statistics = statistics
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> AddTask([FromBody] TaskFormViewModel taskForm)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(taskForm.Description))
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Task description is required",
                        Errors = new List<string> { "Description cannot be empty" }
                    });
                }

                // Create new task
                var newTask = new TaskViewModel
                {
                    TaskId = _nextTaskId++,
                    Description = taskForm.Description.Trim(),
                    IsUrgent = taskForm.IsUrgent,
                    IsImportant = taskForm.IsImportant,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // Simulate async operation
                await Task.Delay(300);

                // Add to mock storage
                _mockTasks.Add(newTask);

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = "Task added successfully!",
                    Task = newTask,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while adding the task",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTask([FromBody] TaskViewModel task)
        {
            try
            {
                var existingTask = _mockTasks.FirstOrDefault(t => t.TaskId == task.TaskId);
                
                if (existingTask == null)
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Task not found",
                        Errors = new List<string> { "The task you're trying to update doesn't exist" }
                    });
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(task.Description))
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Task description is required",
                        Errors = new List<string> { "Description cannot be empty" }
                    });
                }

                // Update task properties
                existingTask.Description = task.Description.Trim();
                existingTask.IsUrgent = task.IsUrgent;
                existingTask.IsImportant = task.IsImportant;
                existingTask.UpdatedAt = DateTime.Now;

                // Simulate async operation
                await Task.Delay(200);

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = "Task updated successfully!",
                    Task = existingTask,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while updating the task",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTask(int taskId)
        {
            try
            {
                var taskToDelete = _mockTasks.FirstOrDefault(t => t.TaskId == taskId);
                
                if (taskToDelete == null)
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Task not found",
                        Errors = new List<string> { "The task you're trying to delete doesn't exist" }
                    });
                }

                // Simulate async operation
                await Task.Delay(200);

                // Remove from mock storage
                _mockTasks.Remove(taskToDelete);

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = "Task deleted successfully!",
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while deleting the task",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> ToggleTaskCompletion(int taskId)
        {
            try
            {
                var task = _mockTasks.FirstOrDefault(t => t.TaskId == taskId);
                
                if (task == null)
                {
                    return Json(new TaskResponseViewModel
                    {
                        Success = false,
                        Message = "Task not found"
                    });
                }

                // Toggle completion status
                task.IsCompleted = !task.IsCompleted;
                task.CompletedAt = task.IsCompleted ? DateTime.Now : null;
                task.UpdatedAt = DateTime.Now;

                // Simulate async operation
                await Task.Delay(150);

                return Json(new TaskResponseViewModel
                {
                    Success = true,
                    Message = task.IsCompleted ? "Task marked as completed!" : "Task marked as pending!",
                    Task = task,
                    ActionDate = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new TaskResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while updating the task",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTaskStatistics()
        {
            var statistics = CalculateStatistics(_mockTasks);
            return Json(statistics);
        }

        private void InitializeMockData()
        {
            _mockTasks.AddRange(new List<TaskViewModel>
            {
                new TaskViewModel
                {
                    TaskId = _nextTaskId++,
                    Description = "Fix critical production bug",
                    IsUrgent = true,
                    IsImportant = true,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now.AddHours(-2),
                    UpdatedAt = DateTime.Now.AddHours(-2)
                },
                new TaskViewModel
                {
                    TaskId = _nextTaskId++,
                    Description = "Plan next quarter strategy",
                    IsUrgent = false,
                    IsImportant = true,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now.AddHours(-4),
                    UpdatedAt = DateTime.Now.AddHours(-4)
                },
                new TaskViewModel
                {
                    TaskId = _nextTaskId++,
                    Description = "Respond to client email",
                    IsUrgent = true,
                    IsImportant = false,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now.AddHours(-1),
                    UpdatedAt = DateTime.Now.AddHours(-1)
                },
                new TaskViewModel
                {
                    TaskId = _nextTaskId++,
                    Description = "Check social media notifications",
                    IsUrgent = false,
                    IsImportant = false,
                    IsCompleted = true,
                    CreatedAt = DateTime.Now.AddHours(-3),
                    UpdatedAt = DateTime.Now.AddMinutes(-30),
                    CompletedAt = DateTime.Now.AddMinutes(-30)
                },
                new TaskViewModel
                {
                    TaskId = _nextTaskId++,
                    Description = "Review team performance reports",
                    IsUrgent = false,
                    IsImportant = true,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now.AddHours(-6),
                    UpdatedAt = DateTime.Now.AddHours(-6)
                }
            });
        }

        private TaskStatisticsViewModel CalculateStatistics(List<TaskViewModel> tasks)
        {
            return new TaskStatisticsViewModel
            {
                TotalTasks = tasks.Count,
                CompletedTasks = tasks.Count(t => t.IsCompleted),
                PendingTasks = tasks.Count(t => !t.IsCompleted),
                UrgentImportantCount = tasks.Count(t => t.IsUrgent && t.IsImportant),
                NotUrgentImportantCount = tasks.Count(t => !t.IsUrgent && t.IsImportant),
                UrgentNotImportantCount = tasks.Count(t => t.IsUrgent && !t.IsImportant),
                NotUrgentNotImportantCount = tasks.Count(t => !t.IsUrgent && !t.IsImportant)
            };
        }
    }
}