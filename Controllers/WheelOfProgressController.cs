using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class WheelOfProgressController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Simulate loading life areas with their categories and progress data
            var viewModel = new WheelOfProgressPageViewModel
            {
                LifeAreas = GetMockProgressData(),
                LastUpdated = DateTime.Now.AddDays(-2)
            };

            // Calculate global progress percentage
            viewModel.GlobalProgressPercentage = CalculateGlobalProgress(viewModel.LifeAreas);
            
            // Calculate statistics
            viewModel.TotalCategories = viewModel.LifeAreas.SelectMany(a => a.Categories).Count();
            viewModel.CategoriesWithGoals = viewModel.LifeAreas
                .SelectMany(a => a.Categories)
                .Count(c => !string.IsNullOrWhiteSpace(c.GoalText));

            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> SaveProgressData([FromBody] List<ProgressItemViewModel> progressData)
        {
            try
            {
                // Simulate validation
                var errors = new List<string>();
                
                foreach (var item in progressData)
                {
                    if (string.IsNullOrWhiteSpace(item.GoalText))
                    {
                        errors.Add($"Goal is required for category ID {item.CategoryId}");
                    }
                    
                    if (item.ProgressPercentage < 0 || item.ProgressPercentage > 100)
                    {
                        errors.Add($"Progress percentage must be between 0-100 for category ID {item.CategoryId}");
                    }
                }

                if (errors.Any())
                {
                    return Json(new SaveProgressResponseViewModel
                    {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }

                // Simulate saving to database
                await Task.Delay(500); // Simulate async operation

                // Calculate new global progress
                var newGlobalProgress = progressData.Any() 
                    ? Math.Round(progressData.Average(p => p.ProgressPercentage), 0) 
                    : 0;

                return Json(new SaveProgressResponseViewModel
                {
                    Success = true,
                    Message = "Progress saved successfully!",
                    NewGlobalProgressPercentage = (decimal)newGlobalProgress,
                    SavedAt = DateTime.Now,
                    TotalCategoriesUpdated = progressData.Count
                });
            }
            catch (Exception ex)
            {
                return Json(new SaveProgressResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while saving progress",
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetChartData()
        {
            var areas = GetMockProgressData();
            
            var chartData = new ProgressChartDataViewModel
            {
                AreaNames = areas.Select(a => a.AreaName).ToList(),
                AreaScores = areas.Select(a => a.AreaScore).ToList(),
                AreaColors = areas.Select(a => a.AreaColor).ToList(),
                GlobalProgressPercentage = CalculateGlobalProgress(areas),
                ChartTitle = "Wheel of Progress"
            };

            return Json(chartData);
        }

        private List<AreaProgressViewModel> GetMockProgressData()
        {
            return new List<AreaProgressViewModel>
            {
                new AreaProgressViewModel
                {
                    AreaId = 1,
                    AreaName = "Physical Health",
                    AreaSlug = "physical-health",
                    AreaColor = "#ff6b6b",
                    AreaIcon = "💪",
                    AreaScore = 8,
                    OrderIndex = 1,
                    Categories = new List<CategoryProgressViewModel>
                    {
                        new CategoryProgressViewModel
                        {
                            CategoryId = 101,
                            AreaId = 1,
                            CategoryName = "Exercise & Fitness",
                            CategorySlug = "exercise-fitness",
                            CategoryColor = "#ffe8e8",
                            GoalText = "Exercise 4 times per week for 45 minutes",
                            ProgressPercentage = 75,
                            NextActionText = "Schedule gym sessions for next week",
                            DeadlineDate = DateTime.Now.AddDays(30),
                            OrderIndex = 1
                        },
                        new CategoryProgressViewModel
                        {
                            CategoryId = 102,
                            AreaId = 1,
                            CategoryName = "Nutrition & Diet",
                            CategorySlug = "nutrition-diet",
                            CategoryColor = "#ffe8e8",
                            GoalText = "Follow meal prep plan and drink 8 glasses of water daily",
                            ProgressPercentage = 60,
                            NextActionText = "Grocery shopping for healthy meals",
                            DeadlineDate = DateTime.Now.AddDays(14),
                            OrderIndex = 2
                        }
                    }
                },
                new AreaProgressViewModel
                {
                    AreaId = 2,
                    AreaName = "Mental Health",
                    AreaSlug = "mental-health",
                    AreaColor = "#4ecdc4",
                    AreaIcon = "🧠",
                    AreaScore = 7,
                    OrderIndex = 2,
                    Categories = new List<CategoryProgressViewModel>
                    {
                        new CategoryProgressViewModel
                        {
                            CategoryId = 201,
                            AreaId = 2,
                            CategoryName = "Stress Management",
                            CategorySlug = "stress-management",
                            CategoryColor = "#e8fffe",
                            GoalText = "Practice meditation 15 minutes daily",
                            ProgressPercentage = 85,
                            NextActionText = "Download guided meditation app",
                            DeadlineDate = DateTime.Now.AddDays(7),
                            OrderIndex = 1
                        },
                        new CategoryProgressViewModel
                        {
                            CategoryId = 202,
                            AreaId = 2,
                            CategoryName = "Work-Life Balance",
                            CategorySlug = "work-life-balance",
                            CategoryColor = "#e8fffe",
                            GoalText = "No work emails after 7 PM",
                            ProgressPercentage = 40,
                            NextActionText = "Set up email auto-response for after hours",
                            DeadlineDate = DateTime.Now.AddDays(21),
                            OrderIndex = 2
                        }
                    }
                },
                new AreaProgressViewModel
                {
                    AreaId = 3,
                    AreaName = "Career & Work",
                    AreaSlug = "career-work",
                    AreaColor = "#45b7d1",
                    AreaIcon = "💼",
                    AreaScore = 9,
                    OrderIndex = 3,
                    Categories = new List<CategoryProgressViewModel>
                    {
                        new CategoryProgressViewModel
                        {
                            CategoryId = 301,
                            AreaId = 3,
                            CategoryName = "Professional Development",
                            CategorySlug = "professional-development",
                            CategoryColor = "#e8f4fd",
                            GoalText = "Complete online certification course",
                            ProgressPercentage = 90,
                            NextActionText = "Schedule final exam",
                            DeadlineDate = DateTime.Now.AddDays(10),
                            OrderIndex = 1
                        },
                        new CategoryProgressViewModel
                        {
                            CategoryId = 302,
                            AreaId = 3,
                            CategoryName = "Networking",
                            CategorySlug = "networking",
                            CategoryColor = "#e8f4fd",
                            GoalText = "Attend 2 industry events monthly",
                            ProgressPercentage = 50,
                            NextActionText = "Register for upcoming conference",
                            DeadlineDate = DateTime.Now.AddDays(45),
                            OrderIndex = 2
                        }
                    }
                },
                new AreaProgressViewModel
                {
                    AreaId = 4,
                    AreaName = "Financial Wellness",
                    AreaSlug = "financial-wellness",
                    AreaColor = "#96ceb4",
                    AreaIcon = "💰",
                    AreaScore = 7,
                    OrderIndex = 4,
                    Categories = new List<CategoryProgressViewModel>
                    {
                        new CategoryProgressViewModel
                        {
                            CategoryId = 401,
                            AreaId = 4,
                            CategoryName = "Emergency Fund",
                            CategorySlug = "emergency-fund",
                            CategoryColor = "#f0f9f4",
                            GoalText = "Save $10,000 emergency fund",
                            ProgressPercentage = 65,
                            NextActionText = "Set up automatic transfer to savings",
                            DeadlineDate = DateTime.Now.AddDays(120),
                            OrderIndex = 1
                        },
                        new CategoryProgressViewModel
                        {
                            CategoryId = 402,
                            AreaId = 4,
                            CategoryName = "Investment Portfolio",
                            CategorySlug = "investment-portfolio",
                            CategoryColor = "#f0f9f4",
                            GoalText = "Diversify investment portfolio across 5 sectors",
                            ProgressPercentage = 30,
                            NextActionText = "Research technology sector ETFs",
                            DeadlineDate = DateTime.Now.AddDays(90),
                            OrderIndex = 2
                        }
                    }
                },
                new AreaProgressViewModel
                {
                    AreaId = 5,
                    AreaName = "Relationships",
                    AreaSlug = "relationships",
                    AreaColor = "#f7b731",
                    AreaIcon = "❤️",
                    AreaScore = 8,
                    OrderIndex = 5,
                    Categories = new List<CategoryProgressViewModel>
                    {
                        new CategoryProgressViewModel
                        {
                            CategoryId = 501,
                            AreaId = 5,
                            CategoryName = "Family Time",
                            CategorySlug = "family-time",
                            CategoryColor = "#fef7e6",
                            GoalText = "Weekly family dinner every Sunday",
                            ProgressPercentage = 80,
                            NextActionText = "Plan this Sunday's dinner menu",
                            DeadlineDate = DateTime.Now.AddDays(3),
                            OrderIndex = 1
                        },
                        new CategoryProgressViewModel
                        {
                            CategoryId = 502,
                            AreaId = 5,
                            CategoryName = "Friendships",
                            CategorySlug = "friendships",
                            CategoryColor = "#fef7e6",
                            GoalText = "Reconnect with 3 old friends this quarter",
                            ProgressPercentage = 33,
                            NextActionText = "Send message to college roommate",
                            DeadlineDate = DateTime.Now.AddDays(60),
                            OrderIndex = 2
                        }
                    }
                },
                new AreaProgressViewModel
                {
                    AreaId = 6,
                    AreaName = "Personal Growth",
                    AreaSlug = "personal-growth",
                    AreaColor = "#a55eea",
                    AreaIcon = "🌱",
                    AreaScore = 8,
                    OrderIndex = 6,
                    Categories = new List<CategoryProgressViewModel>
                    {
                        new CategoryProgressViewModel
                        {
                            CategoryId = 601,
                            AreaId = 6,
                            CategoryName = "Learning & Education",
                            CategorySlug = "learning-education",
                            CategoryColor = "#f3eeff",
                            GoalText = "Read 2 books per month",
                            ProgressPercentage = 70,
                            NextActionText = "Start reading 'Atomic Habits'",
                            DeadlineDate = DateTime.Now.AddDays(15),
                            OrderIndex = 1
                        },
                        new CategoryProgressViewModel
                        {
                            CategoryId = 602,
                            AreaId = 6,
                            CategoryName = "Skill Development",
                            CategorySlug = "skill-development",
                            CategoryColor = "#f3eeff",
                            GoalText = "Learn Spanish conversation basics",
                            ProgressPercentage = 25,
                            NextActionText = "Download language learning app",
                            DeadlineDate = DateTime.Now.AddDays(180),
                            OrderIndex = 2
                        }
                    }
                }
            };
        }

        private decimal CalculateGlobalProgress(List<AreaProgressViewModel> areas)
        {
            if (!areas.Any())
                return 0;

            var allCategories = areas.SelectMany(a => a.Categories).ToList();
            
            if (!allCategories.Any())
                return 0;

            return Math.Round((decimal)allCategories.Average(c => c.ProgressPercentage), 0);
        }
    }
}