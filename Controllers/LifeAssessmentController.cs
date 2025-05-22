using Microsoft.AspNetCore.Mvc;
using ToolBox.Models;

namespace ToolBox.Controllers
{
    public class LifeAssessmentController : Controller
    {
        private readonly ILogger<LifeAssessmentController> _logger;

        public LifeAssessmentController(ILogger<LifeAssessmentController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var lifeAreas = GetLifeAreas();
                
                var model = new LifeAssessmentIndexViewModel
                {
                    LifeAreas = lifeAreas,
                    Questions = new List<QuestionItemViewModel>(), // Empty initially, loaded via AJAX
                    SelectedAreaId = null
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Life Assessment index page");
                return View(new LifeAssessmentIndexViewModel());
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetQuestionsByArea(int areaId)
        {
            try
            {
                var lifeAreas = GetLifeAreas();
                var selectedArea = lifeAreas.FirstOrDefault(a => a.Id == areaId);
                
                if (selectedArea == null)
                {
                    return Json(new GetQuestionsResponseViewModel
                    {
                        Success = false,
                        Message = "Area not found",
                        Questions = new List<QuestionItemViewModel>()
                    });
                }

                var questions = GetQuestionsByAreaId(areaId);

                return Json(new GetQuestionsResponseViewModel
                {
                    Success = true,
                    Message = "Questions loaded successfully",
                    Questions = questions,
                    AreaId = areaId,
                    AreaName = selectedArea.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting questions for area {AreaId}", areaId);
                return Json(new GetQuestionsResponseViewModel
                {
                    Success = false,
                    Message = "Error loading questions",
                    Questions = new List<QuestionItemViewModel>()
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> SubmitAnswers([FromBody] List<QuestionAnswerViewModel> answers)
        {
            try
            {
                // Validate the model
                if (answers == null || !answers.Any())
                {
                    return Json(new SubmitAnswersResponseViewModel
                    {
                        Success = false,
                        Message = "No answers provided",
                        Errors = new List<string> { "Please provide answers to submit" }
                    });
                }

                // Validate each answer
                var errors = new List<string>();
                foreach (var answer in answers)
                {
                    if (answer.AnswerValue < 1 || answer.AnswerValue > 10)
                    {
                        errors.Add($"Answer for question {answer.QuestionId} must be between 1 and 10");
                    }
                }

                if (errors.Any())
                {
                    return Json(new SubmitAnswersResponseViewModel
                    {
                        Success = false,
                        Message = "Validation errors occurred",
                        Errors = errors
                    });
                }

                // Simulate processing and saving the answers
                // In a real application, you would save these to a database
                await Task.Delay(500); // Simulate processing time

                var totalQuestions = answers.Count;
                var averageScore = answers.Average(a => a.AnswerValue);
                var areaId = answers.First().AreaId;
                var lifeAreas = GetLifeAreas();
                var areaName = lifeAreas.FirstOrDefault(a => a.Id == areaId)?.Name ?? "Unknown Area";

                _logger.LogInformation("Life Assessment answers submitted for area {AreaId}: {AnswerCount} answers with average score {AverageScore}", 
                    areaId, totalQuestions, averageScore);

                return Json(new SubmitAnswersResponseViewModel
                {
                    Success = true,
                    Message = "Answers submitted successfully!",
                    TotalQuestions = totalQuestions,
                    AnsweredQuestions = totalQuestions,
                    AverageScore = Math.Round(averageScore, 2),
                    AreaName = areaName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting life assessment answers");
                return Json(new SubmitAnswersResponseViewModel
                {
                    Success = false,
                    Message = "An error occurred while submitting your answers",
                    Errors = new List<string> { "Please try again later" }
                });
            }
        }

        #region Private Helper Methods

        private List<AreaOfLifeFilterItem> GetLifeAreas()
        {
            // Sample life areas - in a real application, these would come from a database
            return new List<AreaOfLifeFilterItem>
            {
                new AreaOfLifeFilterItem
                {
                    Id = 1,
                    Name = "Physical Health",
                    Slug = "physical-health",
                    Icon = "💪",
                    Description = "Questions about your physical well-being, fitness, and health habits",
                    QuestionCount = 8
                },
                new AreaOfLifeFilterItem
                {
                    Id = 2,
                    Name = "Mental Health",
                    Slug = "mental-health",
                    Icon = "🧠",
                    Description = "Questions about your mental well-being, stress management, and emotional health",
                    QuestionCount = 7
                },
                new AreaOfLifeFilterItem
                {
                    Id = 3,
                    Name = "Career & Work",
                    Slug = "career-work",
                    Icon = "💼",
                    Description = "Questions about your professional life, career satisfaction, and work-life balance",
                    QuestionCount = 10
                },
                new AreaOfLifeFilterItem
                {
                    Id = 4,
                    Name = "Financial Wellness",
                    Slug = "financial-wellness",
                    Icon = "💰",
                    Description = "Questions about your financial health, planning, and money management",
                    QuestionCount = 9
                },
                new AreaOfLifeFilterItem
                {
                    Id = 5,
                    Name = "Relationships",
                    Slug = "relationships",
                    Icon = "❤️",
                    Description = "Questions about your relationships with family, friends, and romantic partners",
                    QuestionCount = 8
                },
                new AreaOfLifeFilterItem
                {
                    Id = 6,
                    Name = "Personal Growth",
                    Slug = "personal-growth",
                    Icon = "🌱",
                    Description = "Questions about your personal development, learning, and self-improvement",
                    QuestionCount = 6
                },
                new AreaOfLifeFilterItem
                {
                    Id = 7,
                    Name = "Spiritual Life",
                    Slug = "spiritual-life",
                    Icon = "🙏",
                    Description = "Questions about your spiritual well-being, purpose, and inner peace",
                    QuestionCount = 5
                },
                new AreaOfLifeFilterItem
                {
                    Id = 8,
                    Name = "Recreation & Fun",
                    Slug = "recreation-fun",
                    Icon = "🎯",
                    Description = "Questions about your hobbies, leisure activities, and work-life balance",
                    QuestionCount = 6
                }
            };
        }

        private List<QuestionItemViewModel> GetQuestionsByAreaId(int areaId)
        {
            // Sample questions for each life area - in a real application, these would come from a database
            var questionsByArea = new Dictionary<int, List<QuestionItemViewModel>>
            {
                [1] = new List<QuestionItemViewModel> // Physical Health
                {
                    new QuestionItemViewModel { QuestionId = 101, QuestionText = "I exercise regularly and maintain good physical fitness.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 102, QuestionText = "I eat a balanced and nutritious diet most of the time.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 103, QuestionText = "I get adequate sleep (7-9 hours) on a regular basis.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 104, QuestionText = "I avoid harmful substances like excessive alcohol or tobacco.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 105, QuestionText = "I have regular medical check-ups and preventive care.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 106, QuestionText = "I manage stress effectively and practice relaxation techniques.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 6 },
                    new QuestionItemViewModel { QuestionId = 107, QuestionText = "I maintain good hygiene and take care of my appearance.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 7 },
                    new QuestionItemViewModel { QuestionId = 108, QuestionText = "I listen to my body and rest when I need to recover.", AreaId = 1, AreaName = "Physical Health", OrderIndex = 8 }
                },
                [2] = new List<QuestionItemViewModel> // Mental Health
                {
                    new QuestionItemViewModel { QuestionId = 201, QuestionText = "I feel emotionally balanced and stable most of the time.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 202, QuestionText = "I can manage stress and anxiety effectively.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 203, QuestionText = "I have healthy coping mechanisms for difficult situations.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 204, QuestionText = "I feel confident and have good self-esteem.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 205, QuestionText = "I practice mindfulness or meditation regularly.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 206, QuestionText = "I seek help when I need emotional or mental support.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 6 },
                    new QuestionItemViewModel { QuestionId = 207, QuestionText = "I maintain a positive outlook and practice gratitude.", AreaId = 2, AreaName = "Mental Health", OrderIndex = 7 }
                },
                [3] = new List<QuestionItemViewModel> // Career & Work
                {
                    new QuestionItemViewModel { QuestionId = 301, QuestionText = "I am satisfied with my current career path and professional growth.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 302, QuestionText = "My work provides me with a sense of purpose and fulfillment.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 303, QuestionText = "I have clear professional goals and am working towards them.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 304, QuestionText = "I maintain a healthy work-life balance.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 305, QuestionText = "I continuously develop my skills and knowledge in my field.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 306, QuestionText = "I have good relationships with my colleagues and supervisors.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 6 },
                    new QuestionItemViewModel { QuestionId = 307, QuestionText = "My income meets my current needs and future goals.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 7 },
                    new QuestionItemViewModel { QuestionId = 308, QuestionText = "I feel valued and appreciated for my contributions at work.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 8 },
                    new QuestionItemViewModel { QuestionId = 309, QuestionText = "I have opportunities for advancement and career development.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 9 },
                    new QuestionItemViewModel { QuestionId = 310, QuestionText = "My work environment is positive and supportive.", AreaId = 3, AreaName = "Career & Work", OrderIndex = 10 }
                },
                [4] = new List<QuestionItemViewModel> // Financial Wellness
                {
                    new QuestionItemViewModel { QuestionId = 401, QuestionText = "I have a clear budget and track my income and expenses.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 402, QuestionText = "I have an emergency fund that covers 3-6 months of expenses.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 403, QuestionText = "I am saving regularly for my retirement and future goals.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 404, QuestionText = "I have manageable debt and a plan to pay it off.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 405, QuestionText = "I have adequate insurance coverage for my needs.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 406, QuestionText = "I make informed decisions about investments and financial planning.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 6 },
                    new QuestionItemViewModel { QuestionId = 407, QuestionText = "I am not stressed about money and feel financially secure.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 7 },
                    new QuestionItemViewModel { QuestionId = 408, QuestionText = "My estate and last will and testament is all in place and up to date.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 8 },
                    new QuestionItemViewModel { QuestionId = 409, QuestionText = "I have clear financial goals and a plan to achieve them.", AreaId = 4, AreaName = "Financial Wellness", OrderIndex = 9 }
                },
                [5] = new List<QuestionItemViewModel> // Relationships
                {
                    new QuestionItemViewModel { QuestionId = 501, QuestionText = "I have strong, supportive relationships with family members.", AreaId = 5, AreaName = "Relationships", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 502, QuestionText = "I maintain meaningful friendships and social connections.", AreaId = 5, AreaName = "Relationships", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 503, QuestionText = "I communicate effectively and resolve conflicts constructively.", AreaId = 5, AreaName = "Relationships", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 504, QuestionText = "I am satisfied with my romantic relationship (if applicable).", AreaId = 5, AreaName = "Relationships", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 505, QuestionText = "I invest time and effort in nurturing my relationships.", AreaId = 5, AreaName = "Relationships", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 506, QuestionText = "I set healthy boundaries in my relationships.", AreaId = 5, AreaName = "Relationships", OrderIndex = 6 },
                    new QuestionItemViewModel { QuestionId = 507, QuestionText = "I feel loved, valued, and supported by the people close to me.", AreaId = 5, AreaName = "Relationships", OrderIndex = 7 },
                    new QuestionItemViewModel { QuestionId = 508, QuestionText = "I contribute positively to my community and social circles.", AreaId = 5, AreaName = "Relationships", OrderIndex = 8 }
                },
                [6] = new List<QuestionItemViewModel> // Personal Growth
                {
                    new QuestionItemViewModel { QuestionId = 601, QuestionText = "I am continuously learning and developing new skills.", AreaId = 6, AreaName = "Personal Growth", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 602, QuestionText = "I have clear personal goals and am working towards them.", AreaId = 6, AreaName = "Personal Growth", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 603, QuestionText = "I regularly reflect on my experiences and learn from them.", AreaId = 6, AreaName = "Personal Growth", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 604, QuestionText = "I challenge myself to step out of my comfort zone.", AreaId = 6, AreaName = "Personal Growth", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 605, QuestionText = "I read books, take courses, or engage in other learning activities.", AreaId = 6, AreaName = "Personal Growth", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 606, QuestionText = "I am open to feedback and use it for self-improvement.", AreaId = 6, AreaName = "Personal Growth", OrderIndex = 6 }
                },
                [7] = new List<QuestionItemViewModel> // Spiritual Life
                {
                    new QuestionItemViewModel { QuestionId = 701, QuestionText = "I have a strong sense of purpose and meaning in my life.", AreaId = 7, AreaName = "Spiritual Life", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 702, QuestionText = "I practice spiritual or religious activities that bring me peace.", AreaId = 7, AreaName = "Spiritual Life", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 703, QuestionText = "I feel connected to something greater than myself.", AreaId = 7, AreaName = "Spiritual Life", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 704, QuestionText = "I practice gratitude and appreciation regularly.", AreaId = 7, AreaName = "Spiritual Life", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 705, QuestionText = "I live according to my values and moral principles.", AreaId = 7, AreaName = "Spiritual Life", OrderIndex = 5 }
                },
                [8] = new List<QuestionItemViewModel> // Recreation & Fun
                {
                    new QuestionItemViewModel { QuestionId = 801, QuestionText = "I regularly engage in hobbies and activities I enjoy.", AreaId = 8, AreaName = "Recreation & Fun", OrderIndex = 1 },
                    new QuestionItemViewModel { QuestionId = 802, QuestionText = "I make time for relaxation and leisure activities.", AreaId = 8, AreaName = "Recreation & Fun", OrderIndex = 2 },
                    new QuestionItemViewModel { QuestionId = 803, QuestionText = "I have fun and laugh regularly in my daily life.", AreaId = 8, AreaName = "Recreation & Fun", OrderIndex = 3 },
                    new QuestionItemViewModel { QuestionId = 804, QuestionText = "I explore new activities and experiences that interest me.", AreaId = 8, AreaName = "Recreation & Fun", OrderIndex = 4 },
                    new QuestionItemViewModel { QuestionId = 805, QuestionText = "I balance productivity with play and enjoyment.", AreaId = 8, AreaName = "Recreation & Fun", OrderIndex = 5 },
                    new QuestionItemViewModel { QuestionId = 806, QuestionText = "I spend quality time in nature or outdoor activities.", AreaId = 8, AreaName = "Recreation & Fun", OrderIndex = 6 }
                }
            };

            return questionsByArea.ContainsKey(areaId) ? questionsByArea[areaId] : new List<QuestionItemViewModel>();
        }

        #endregion
    }
}