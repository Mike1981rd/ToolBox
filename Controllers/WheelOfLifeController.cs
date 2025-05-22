using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class WheelOfLifeController : Controller
    {
        private readonly ILogger<WheelOfLifeController> _logger;

        public WheelOfLifeController(ILogger<WheelOfLifeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var lifeAreas = GetLifeAreaScores();
                var totalScore = lifeAreas.Sum(area => area.CurrentScore);
                var averageScore = lifeAreas.Count > 0 ? Math.Round((double)totalScore / lifeAreas.Count, 2) : 0;
                var maxPossibleScore = lifeAreas.Count * 10;

                var model = new WheelOfLifePageViewModel
                {
                    LifeAreas = lifeAreas,
                    TotalScore = totalScore,
                    AverageScore = averageScore,
                    MaxPossibleScore = maxPossibleScore,
                    LastUpdated = DateTime.Now.ToString("MMM dd, yyyy")
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Wheel of Life index page");
                return View(new WheelOfLifePageViewModel());
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveWheelScores([FromBody] SaveWheelScoresRequestViewModel request)
        {
            try
            {
                // Validate the model
                if (request?.Scores == null || !request.Scores.Any())
                {
                    return Json(new SaveWheelScoresResponseViewModel
                    {
                        Success = false,
                        Message = "No scores provided",
                        Errors = new List<string> { "Please provide scores to save" }
                    });
                }

                // Validate each score
                var errors = new List<string>();
                foreach (var score in request.Scores)
                {
                    if (score.CurrentScore < 1 || score.CurrentScore > 10)
                    {
                        errors.Add($"Score for {score.AreaName} must be between 1 and 10");
                    }
                }

                if (errors.Any())
                {
                    return Json(new SaveWheelScoresResponseViewModel
                    {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }

                // Simulate processing and saving the scores
                // In a real application, you would save these to a database
                await Task.Delay(300); // Simulate processing time

                var totalScore = request.Scores.Sum(s => s.CurrentScore);
                var averageScore = Math.Round((double)totalScore / request.Scores.Count, 2);

                _logger.LogInformation("Wheel of Life scores saved: {ScoreCount} areas with total score {TotalScore}", 
                    request.Scores.Count, totalScore);

                return Json(new SaveWheelScoresResponseViewModel
                {
                    Success = true,
                    Message = "Scores saved successfully!",
                    TotalScore = totalScore,
                    AverageScore = averageScore,
                    AreasCount = request.Scores.Count,
                    SavedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving wheel of life scores");
                return Json(new SaveWheelScoresResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while saving your scores",
                    Errors = new List<string> { "Please try again later" }
                });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetChartData()
        {
            try
            {
                var lifeAreas = GetLifeAreaScores();
                
                var chartData = new ChartDataViewModel
                {
                    Labels = lifeAreas.Select(area => area.AreaName).ToList(),
                    Data = lifeAreas.Select(area => area.CurrentScore).ToList(),
                    BackgroundColors = lifeAreas.Select(area => ConvertToRgba(area.AreaColor, 0.6)).ToList(),
                    BorderColors = lifeAreas.Select(area => area.AreaColor).ToList(),
                    ChartType = "radar"
                };

                return Json(new { success = true, data = chartData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting chart data");
                return Json(new { success = false, message = "Error loading chart data" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetWheelStats()
        {
            try
            {
                var lifeAreas = GetLifeAreaScores();
                
                var stats = new WheelStatsViewModel
                {
                    TotalAssessments = 1,
                    TotalAreas = lifeAreas.Count,
                    OverallAverageScore = lifeAreas.Count > 0 ? Math.Round(lifeAreas.Average(a => a.CurrentScore), 2) : 0,
                    LastUpdated = DateTime.Now,
                    AreaStats = lifeAreas.Select(area => new AreaStatViewModel
                    {
                        AreaId = area.AreaId,
                        AreaName = area.AreaName,
                        AverageScore = area.CurrentScore,
                        HighestScore = area.CurrentScore,
                        LowestScore = area.CurrentScore,
                        Icon = area.AreaIcon,
                        Color = area.AreaColor
                    }).ToList()
                };

                return Json(new { success = true, stats = stats });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting wheel statistics");
                return Json(new { success = false, message = "Error loading statistics" });
            }
        }

        #region Private Helper Methods

        private List<LifeAreaScoreViewModel> GetLifeAreaScores()
        {
            // Sample life areas with initial scores - in a real application, these would come from a database
            return new List<LifeAreaScoreViewModel>
            {
                new LifeAreaScoreViewModel
                {
                    AreaId = 1,
                    AreaName = "Physical Health",
                    AreaSlug = "physical-health",
                    AreaIcon = "💪",
                    AreaColor = "#FF6B6B",
                    AreaDescription = "Your physical well-being, fitness, and health habits",
                    CurrentScore = 6,
                    OrderIndex = 1
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 2,
                    AreaName = "Mental Health",
                    AreaSlug = "mental-health",
                    AreaIcon = "🧠",
                    AreaColor = "#4ECDC4",
                    AreaDescription = "Your mental well-being, stress management, and emotional health",
                    CurrentScore = 7,
                    OrderIndex = 2
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 3,
                    AreaName = "Career & Work",
                    AreaSlug = "career-work",
                    AreaIcon = "💼",
                    AreaColor = "#45B7D1",
                    AreaDescription = "Your professional life, career satisfaction, and work-life balance",
                    CurrentScore = 8,
                    OrderIndex = 3
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 4,
                    AreaName = "Financial Wellness",
                    AreaSlug = "financial-wellness",
                    AreaIcon = "💰",
                    AreaColor = "#F7DC6F",
                    AreaDescription = "Your financial health, planning, and money management",
                    CurrentScore = 5,
                    OrderIndex = 4
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 5,
                    AreaName = "Relationships",
                    AreaSlug = "relationships",
                    AreaIcon = "❤️",
                    AreaColor = "#BB8FCE",
                    AreaDescription = "Your relationships with family, friends, and romantic partners",
                    CurrentScore = 9,
                    OrderIndex = 5
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 6,
                    AreaName = "Personal Growth",
                    AreaSlug = "personal-growth",
                    AreaIcon = "🌱",
                    AreaColor = "#85C1E9",
                    AreaDescription = "Your personal development, learning, and self-improvement",
                    CurrentScore = 7,
                    OrderIndex = 6
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 7,
                    AreaName = "Spiritual Life",
                    AreaSlug = "spiritual-life",
                    AreaIcon = "🙏",
                    AreaColor = "#A9DFBF",
                    AreaDescription = "Your spiritual well-being, purpose, and inner peace",
                    CurrentScore = 6,
                    OrderIndex = 7
                },
                new LifeAreaScoreViewModel
                {
                    AreaId = 8,
                    AreaName = "Recreation & Fun",
                    AreaSlug = "recreation-fun",
                    AreaIcon = "🎯",
                    AreaColor = "#F8C471",
                    AreaDescription = "Your hobbies, leisure activities, and work-life balance",
                    CurrentScore = 8,
                    OrderIndex = 8
                }
            };
        }

        private string ConvertToRgba(string hexColor, double opacity)
        {
            // Convert hex color to RGBA with specified opacity
            if (string.IsNullOrEmpty(hexColor) || !hexColor.StartsWith("#"))
            {
                return $"rgba(102, 126, 234, {opacity})"; // Default color
            }

            try
            {
                var hex = hexColor.TrimStart('#');
                if (hex.Length == 6)
                {
                    var r = Convert.ToInt32(hex.Substring(0, 2), 16);
                    var g = Convert.ToInt32(hex.Substring(2, 2), 16);
                    var b = Convert.ToInt32(hex.Substring(4, 2), 16);
                    return $"rgba({r}, {g}, {b}, {opacity})";
                }
            }
            catch
            {
                // Fallback to default color if conversion fails
            }

            return $"rgba(102, 126, 234, {opacity})";
        }

        #endregion
    }
}