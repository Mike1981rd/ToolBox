using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ToolBox.Data;
using ToolBox.Interfaces;
using ToolBox.Models;
using ToolBox.Models.ViewModels;

namespace ToolBox.Services
{
    public class Feedback360Service : IFeedback360Service
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly ILogger<Feedback360Service> _logger;

        public Feedback360Service(
            ApplicationDbContext context,
            IEmailSender emailSender,
            INotificationService notificationService,
            IUserService userService,
            ILogger<Feedback360Service> logger)
        {
            _context = context;
            _emailSender = emailSender;
            _notificationService = notificationService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<IEnumerable<Feedback360Instance>> GetInstancesByCoachAsync(int coachId)
        {
            return await _context.Feedback360Instances
                .Include(i => i.SubjectUser)
                .Include(i => i.Raters)
                .Where(i => i.InitiatedByCoachId == coachId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<Feedback360Instance?> GetInstanceByIdAsync(long instanceId, bool includeRaters = false)
        {
            var query = _context.Feedback360Instances
                .Include(i => i.SubjectUser)
                .Include(i => i.InitiatedByCoach)
                .AsQueryable();

            if (includeRaters)
            {
                query = query.Include(i => i.Raters)
                            .ThenInclude(r => r.RaterUser);
            }

            return await query.FirstOrDefaultAsync(i => i.Id == instanceId);
        }

        public async Task<Feedback360Instance> CreateInstanceAsync(int subjectUserId, int coachId, string title, DateTime deadline)
        {
            var instance = new Feedback360Instance
            {
                SubjectUserId = subjectUserId,
                InitiatedByCoachId = coachId,
                InstanceTitle = title,
                FeedbackDeadline = deadline.Kind == DateTimeKind.Utc ? deadline : deadline.ToUniversalTime(),
                Status = Feedback360Status.PendingSetup,
                CreatedAt = DateTime.UtcNow
            };

            _context.Feedback360Instances.Add(instance);
            await _context.SaveChangesAsync();

            return instance;
        }

        public async Task<bool> UpdateInstanceStatusAsync(long instanceId, Feedback360Status newStatus)
        {
            var instance = await _context.Feedback360Instances.FindAsync(instanceId);
            if (instance == null) return false;

            instance.Status = newStatus;
            if (newStatus == Feedback360Status.Completed)
            {
                instance.ReportGeneratedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Feedback360Rater?> AddRaterAsync(long instanceId, string email, string? name, RaterCategory category, int? existingUserId = null)
        {
            // Verificar si el email ya existe para esta instancia
            var existingRater = await _context.Feedback360Raters
                .FirstOrDefaultAsync(r => r.Feedback360InstanceId == instanceId && 
                                        (r.ExternalRaterEmail == email || 
                                         (existingUserId.HasValue && r.RaterUserId == existingUserId)));

            if (existingRater != null)
            {
                return null; // Ya existe
            }

            // Si no se proporcionó un userId, buscar si el email corresponde a un usuario existente
            if (!existingUserId.HasValue)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    existingUserId = user.Id;
                    name = user.FullName;
                }
            }

            var rater = new Feedback360Rater
            {
                Feedback360InstanceId = instanceId,
                RaterUserId = existingUserId,
                ExternalRaterEmail = existingUserId.HasValue ? null : email,
                ExternalRaterName = existingUserId.HasValue ? null : name,
                Category = category,
                Status = RaterStatus.PendingInvitation,
                UniqueResponseToken = Guid.NewGuid().ToString("N")
            };

            _context.Feedback360Raters.Add(rater);
            await _context.SaveChangesAsync();

            return rater;
        }

        public async Task<bool> RemoveRaterAsync(long raterId)
        {
            var rater = await _context.Feedback360Raters.FindAsync(raterId);
            if (rater == null || !await CanRemoveRaterAsync(raterId))
            {
                return false;
            }

            _context.Feedback360Raters.Remove(rater);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CanRemoveRaterAsync(long raterId)
        {
            var rater = await _context.Feedback360Raters.FindAsync(raterId);
            return rater != null && 
                   (rater.Status == RaterStatus.PendingInvitation || 
                    (rater.Status == RaterStatus.Invited && !rater.CompletedAt.HasValue));
        }

        public async Task<IEnumerable<Feedback360Rater>> GetRatersByInstanceAsync(long instanceId)
        {
            return await _context.Feedback360Raters
                .Include(r => r.RaterUser)
                .Where(r => r.Feedback360InstanceId == instanceId)
                .OrderBy(r => r.Category)
                .ThenBy(r => r.ExternalRaterName ?? r.RaterUser!.FullName)
                .ToListAsync();
        }

        public async Task<(bool Success, string Message)> FinalizeAndSendInvitationsAsync(long instanceId)
        {
            try
            {
                var instance = await GetInstanceByIdAsync(instanceId, includeRaters: true);
                if (instance == null)
                {
                    return (false, "Instance not found");
                }

                // Verificar que haya al menos algunos evaluadores
                var raterCount = instance.Raters.Count;
                if (raterCount < 3) // Mínimo: self + manager + 1 peer
                {
                    return (false, "At least 3 raters are required (including self-evaluation)");
                }

                // Verificar que haya autoevaluación
                var hasSelfEvaluation = instance.Raters.Any(r => r.Category == RaterCategory.Self);
                if (!hasSelfEvaluation)
                {
                    // Agregar automáticamente al sujeto como autoevaluador
                    var subject = await _context.Users.FindAsync(instance.SubjectUserId);
                    if (subject != null)
                    {
                        await AddRaterAsync(instanceId, subject.Email, subject.FullName, RaterCategory.Self, subject.Id);
                    }
                }

                // Enviar invitaciones
                foreach (var rater in instance.Raters.Where(r => r.Status == RaterStatus.PendingInvitation))
                {
                    try
                    {
                        // Construir el enlace de respuesta
                        var responseUrl = $"/Feedback360Response/Respond?token={rater.UniqueResponseToken}";
                        
                        var emailTo = rater.RaterUserId.HasValue && rater.RaterUser != null 
                            ? rater.RaterUser.Email 
                            : rater.ExternalRaterEmail;
                            
                        var raterName = rater.RaterUserId.HasValue && rater.RaterUser != null 
                            ? rater.RaterUser.FullName 
                            : rater.ExternalRaterName ?? "Evaluator";

                        // Enviar email
                        await _emailSender.SendEmailAsync(
                            emailTo!,
                            $"Feedback 360° Request for {instance.SubjectUser?.FullName}",
                            $@"Dear {raterName},

You have been invited to provide feedback for {instance.SubjectUser?.FullName} as part of a 360-degree feedback process.

Your feedback is valuable and will help in their professional development. All responses are confidential.

Please complete the feedback by: {instance.FeedbackDeadline:MMMM dd, yyyy}

Click here to provide your feedback: {responseUrl}

Thank you for your participation.

Best regards,
{instance.InitiatedByCoach?.FullName}");

                        // Actualizar estado del evaluador
                        rater.Status = RaterStatus.Invited;
                        rater.InvitedAt = DateTime.UtcNow;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending invitation to {Email}", rater.ExternalRaterEmail ?? rater.RaterUser?.Email);
                    }
                }

                // Actualizar estado de la instancia
                instance.Status = Feedback360Status.CollectingFeedback;
                await _context.SaveChangesAsync();

                // Notificar al sujeto
                await _notificationService.CreateNotificationAsync(
                    instance.SubjectUserId,
                    "feedback_360_process_initiated",
                    new
                    {
                        InstanceTitle = instance.InstanceTitle,
                        CoachName = instance.InitiatedByCoach?.FullName,
                        InstanceId = instance.Id,
                        DeadlineDate = instance.FeedbackDeadline
                    }
                );

                return (true, "Invitations sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finalizing instance {InstanceId}", instanceId);
                return (false, "An error occurred while sending invitations");
            }
        }

        public async Task<Feedback360Rater?> GetRaterByTokenAsync(string token)
        {
            return await _context.Feedback360Raters
                .Include(r => r.Feedback360Instance)
                    .ThenInclude(i => i.SubjectUser)
                .FirstOrDefaultAsync(r => r.UniqueResponseToken == token);
        }

        public async Task<(int Total, int Completed)> GetRaterStatsAsync(long instanceId)
        {
            var raters = await _context.Feedback360Raters
                .Where(r => r.Feedback360InstanceId == instanceId)
                .ToListAsync();

            var total = raters.Count;
            var completed = raters.Count(r => r.Status == RaterStatus.Completed);

            return (total, completed);
        }

        public async Task<Feedback360ReportViewModel> GenerateReportAsync(long instanceId, int requestingUserId)
        {
            // Get instance with all related data
            var instance = await _context.Feedback360Instances
                .Include(i => i.SubjectUser)
                .Include(i => i.InitiatedByCoach)
                .Include(i => i.Raters)
                    .ThenInclude(r => r.ScaleResponses)
                .Include(i => i.Raters)
                    .ThenInclude(r => r.OpenEndedResponses)
                .FirstOrDefaultAsync(i => i.Id == instanceId);

            if (instance == null)
            {
                throw new ArgumentException($"Feedback360 instance {instanceId} not found");
            }

            // Verify permissions
            bool isCoach = instance.InitiatedByCoachId == requestingUserId;
            bool isSubject = instance.SubjectUserId == requestingUserId;
            
            if (!isCoach && !isSubject)
            {
                throw new UnauthorizedAccessException("User does not have permission to view this report");
            }

            // Update instance status if this is the first time generating the report
            if (instance.ReportGeneratedAt == null)
            {
                instance.ReportGeneratedAt = DateTime.UtcNow;
                if (instance.Status == Feedback360Status.CollectingFeedback)
                {
                    instance.Status = Feedback360Status.Completed;
                }
                await _context.SaveChangesAsync();
            }

            // Get competency and question definitions
            var competencies = Feedback360Definitions.GetCompetencies();
            var openQuestions = Feedback360Definitions.GetOpenQuestions();
            var scaleLabels = Feedback360Definitions.GetLikertScaleLabels();

            // Analysis configuration
            var analysisConfig = new ReportAnalysisData();

            // Create report view model
            var report = new Feedback360ReportViewModel
            {
                InstanceId = instance.Id,
                InstanceTitle = instance.InstanceTitle,
                SubjectUserName = instance.SubjectUser?.FullName ?? "Unknown",
                ReportGeneratedAt = instance.ReportGeneratedAt ?? DateTime.UtcNow,
                FeedbackDeadline = instance.FeedbackDeadline,
                IsCoachView = isCoach,
                CanViewDetailedBreakdown = isCoach // Coaches can see more detailed breakdowns
            };

            // Get rater stats
            var (totalRaters, completedRaters) = await GetRaterStatsAsync(instanceId);
            report.TotalRaters = totalRaters;
            report.CompletedRaters = completedRaters;

            // Process competency data
            await ProcessCompetencyReports(instance, competencies, analysisConfig, report);

            // Process open-ended responses
            await ProcessOpenEndedReports(instance, openQuestions, analysisConfig, report);

            // Generate analysis (strengths, development areas, gaps)
            GenerateReportAnalysis(report, analysisConfig);

            // Prepare chart data
            PrepareChartData(report);

            return report;
        }

        public async Task<bool> CanViewReportAsync(long instanceId, int userId, bool isCoach = true)
        {
            var instance = await GetInstanceByIdAsync(instanceId);
            if (instance == null) return false;

            if (isCoach)
            {
                return instance.InitiatedByCoachId == userId;
            }
            else
            {
                // Subject can view their own report if it's completed or coach has released it
                return instance.SubjectUserId == userId && 
                       (instance.Status == Feedback360Status.Completed || 
                        instance.ReportGeneratedAt != null);
            }
        }

        private async Task ProcessCompetencyReports(Feedback360Instance instance, 
            List<CompetencyDefinition> competencies, 
            ReportAnalysisData analysisConfig, 
            Feedback360ReportViewModel report)
        {
            foreach (var competency in competencies)
            {
                var competencyReport = new CompetencyReportViewModel
                {
                    CompetencyCode = competency.Code,
                    CompetencyName = competency.NameEnglish, // TODO: Use user's preferred language
                    CompetencyDescription = "" // No description in current structure
                };

                // Process each question in the competency
                foreach (var question in competency.Questions)
                {
                    var questionReport = new QuestionScaleReportViewModel
                    {
                        QuestionCode = question.Code,
                        QuestionText = question.TextEnglish, // TODO: Use user's preferred language
                        IsRequired = true // All questions are required by default
                    };

                    // Group responses by rater category
                    var responsesByCategory = instance.Raters
                        .Where(r => r.Status == RaterStatus.Completed)
                        .SelectMany(r => r.ScaleResponses.Where(sr => sr.QuestionCode == question.Code))
                        .GroupBy(sr => sr.Feedback360Rater.Category)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    // Calculate averages for each category
                    foreach (var category in Enum.GetValues<RaterCategory>())
                    {
                        if (responsesByCategory.ContainsKey(category))
                        {
                            var responses = responsesByCategory[category];
                            var count = responses.Count;
                            
                            questionReport.ResponseCountByCategory[category] = count;

                            // Apply anonymity rules
                            bool hasSufficientResponses = category switch
                            {
                                RaterCategory.Self => count >= 1,
                                RaterCategory.Manager => count >= 1,
                                RaterCategory.Peer => count >= analysisConfig.MinimumResponsesForPeers,
                                RaterCategory.DirectReport => count >= analysisConfig.MinimumResponsesForDirectReports,
                                _ => count >= analysisConfig.MinimumResponsesForDisplay
                            };

                            questionReport.HasSufficientResponses[category] = hasSufficientResponses;

                            if (hasSufficientResponses)
                            {
                                var average = responses.Average(r => r.Score);
                                questionReport.ScoresByRaterCategory[category] = average;

                                if (category == RaterCategory.Self)
                                {
                                    questionReport.SelfScore = average;
                                }
                            }
                        }
                        else
                        {
                            questionReport.ResponseCountByCategory[category] = 0;
                            questionReport.HasSufficientResponses[category] = false;
                        }
                    }

                    // Calculate overall average (excluding self)
                    var othersScores = questionReport.ScoresByRaterCategory
                        .Where(kvp => kvp.Key != RaterCategory.Self && kvp.Value.HasValue)
                        .Select(kvp => kvp.Value!.Value);

                    if (othersScores.Any())
                    {
                        questionReport.OverallAverageScore = othersScores.Average();
                    }

                    // Calculate gap from self
                    if (questionReport.SelfScore.HasValue && questionReport.OverallAverageScore.HasValue)
                    {
                        questionReport.GapFromSelf = questionReport.SelfScore.Value - questionReport.OverallAverageScore.Value;
                    }

                    competencyReport.QuestionScaleReports.Add(questionReport);
                }

                // Calculate competency-level averages
                foreach (var category in Enum.GetValues<RaterCategory>())
                {
                    var categoryScores = competencyReport.QuestionScaleReports
                        .Where(q => q.ScoresByRaterCategory.ContainsKey(category) && 
                                   q.ScoresByRaterCategory[category].HasValue)
                        .Select(q => q.ScoresByRaterCategory[category]!.Value);

                    if (categoryScores.Any())
                    {
                        competencyReport.CompetencyAveragesByCategory[category] = categoryScores.Average();
                    }
                }

                // Calculate overall competency average (excluding self)
                var overallScores = competencyReport.CompetencyAveragesByCategory
                    .Where(kvp => kvp.Key != RaterCategory.Self && kvp.Value.HasValue)
                    .Select(kvp => kvp.Value!.Value);

                if (overallScores.Any())
                {
                    competencyReport.OverallCompetencyAverage = overallScores.Average();
                }

                report.CompetencyReports.Add(competencyReport);
            }
        }

        private async Task ProcessOpenEndedReports(Feedback360Instance instance, 
            List<OpenQuestionDefinition> openQuestions, 
            ReportAnalysisData analysisConfig, 
            Feedback360ReportViewModel report)
        {
            foreach (var question in openQuestions)
            {
                var openEndedReport = new OpenEndedReportViewModel
                {
                    QuestionCode = question.Code,
                    QuestionText = question.TextEnglish, // TODO: Use user's preferred language
                    IsRequired = true // All questions are required by default
                };

                // Get all responses for this question
                var responses = instance.Raters
                    .Where(r => r.Status == RaterStatus.Completed)
                    .SelectMany(r => r.OpenEndedResponses.Where(or => or.QuestionCode == question.Code))
                    .Where(or => !string.IsNullOrWhiteSpace(or.ResponseText))
                    .ToList();

                openEndedReport.TotalResponseCount = responses.Count;

                // Group by category for potential display
                var responsesByCategory = responses
                    .GroupBy(r => r.Feedback360Rater.Category)
                    .ToList();

                foreach (var categoryGroup in responsesByCategory)
                {
                    var category = categoryGroup.Key;
                    var categoryResponses = categoryGroup.Select(r => r.ResponseText).ToList();
                    
                    // Determine if safe to display by category
                    bool isSafeToDisplay = category switch
                    {
                        RaterCategory.Self => true, // Self is always safe
                        RaterCategory.Manager => true, // Manager is typically safe (usually just one)
                        _ => categoryResponses.Count >= analysisConfig.MinimumResponsesForDisplay
                    };

                    var responseGroup = new AnonymousResponseGroup
                    {
                        CategoryLabel = category.GetDisplayName(),
                        Responses = categoryResponses,
                        IsSafeToDisplay = isSafeToDisplay
                    };

                    openEndedReport.ResponseGroups.Add(responseGroup);
                }

                // For maximum anonymity, also provide all responses mixed together
                openEndedReport.AllAnonymousResponses = responses
                    .Select(r => r.ResponseText)
                    .OrderBy(r => Guid.NewGuid()) // Shuffle for anonymity
                    .ToList();

                report.OpenEndedFeedback.Add(openEndedReport);
            }
        }

        private void GenerateReportAnalysis(Feedback360ReportViewModel report, ReportAnalysisData analysisConfig)
        {
            var competencyAverages = new List<(string Name, double Score)>();

            foreach (var competency in report.CompetencyReports)
            {
                if (competency.OverallCompetencyAverage.HasValue)
                {
                    competencyAverages.Add((competency.CompetencyName, competency.OverallCompetencyAverage.Value));

                    // Mark as strength or development area
                    if (competency.OverallCompetencyAverage.Value >= analysisConfig.StrengthThreshold)
                    {
                        competency.IsStrength = true;
                    }
                    else if (competency.OverallCompetencyAverage.Value < analysisConfig.DevelopmentThreshold)
                    {
                        competency.IsDevelopmentArea = true;
                    }

                    // Check for significant gaps between self and others
                    var selfAverage = competency.CompetencyAveragesByCategory.GetValueOrDefault(RaterCategory.Self);
                    if (selfAverage.HasValue)
                    {
                        var gap = selfAverage.Value - competency.OverallCompetencyAverage.Value;
                        if (Math.Abs(gap) >= analysisConfig.SignificantGapThreshold)
                        {
                            competency.HasSignificantGap = true;
                            competency.GapDescription = gap > 0 
                                ? $"Self-rating is {gap:F1} points higher than others' average"
                                : $"Self-rating is {Math.Abs(gap):F1} points lower than others' average";
                        }
                    }
                }
            }

            // Identify top strengths and development areas
            report.IdentifiedStrengths = competencyAverages
                .Where(c => c.Score >= analysisConfig.StrengthThreshold)
                .OrderByDescending(c => c.Score)
                .Take(5)
                .Select(c => c.Name)
                .ToList();

            report.IdentifiedDevelopmentAreas = competencyAverages
                .Where(c => c.Score < analysisConfig.DevelopmentThreshold)
                .OrderBy(c => c.Score)
                .Take(5)
                .Select(c => c.Name)
                .ToList();

            // Identify significant gaps
            report.SignificantGaps = report.CompetencyReports
                .Where(c => c.HasSignificantGap)
                .Select(c => $"{c.CompetencyName}: {c.GapDescription}")
                .ToList();
        }

        private void PrepareChartData(Feedback360ReportViewModel report)
        {
            // Prepare overall comparison chart data
            var competencyNames = report.CompetencyReports.Select(c => c.CompetencyName).ToList();
            report.OverallComparisonLabels = competencyNames;

            // Create series for different rater categories
            var categories = new[] { RaterCategory.Self, RaterCategory.Manager, RaterCategory.Peer, RaterCategory.DirectReport };

            foreach (var category in categories)
            {
                var seriesData = new List<double?>();
                bool hasAnyData = false;

                foreach (var competency in report.CompetencyReports)
                {
                    if (competency.CompetencyAveragesByCategory.ContainsKey(category))
                    {
                        seriesData.Add(competency.CompetencyAveragesByCategory[category]);
                        if (competency.CompetencyAveragesByCategory[category].HasValue)
                        {
                            hasAnyData = true;
                        }
                    }
                    else
                    {
                        seriesData.Add(null);
                    }
                }

                // Only add series if there's data
                if (hasAnyData)
                {
                    report.OverallComparisonSeries.Add(new ReportSeriesData
                    {
                        SeriesLabel = category.GetDisplayName(),
                        Data = seriesData,
                        BackgroundColor = category.GetChartColor(),
                        BorderColor = category.GetChartColor()
                    });
                }
            }

            // Prepare competency-specific chart data
            foreach (var competency in report.CompetencyReports)
            {
                var questionNames = competency.QuestionScaleReports.Select(q => 
                    q.QuestionText.Length > 50 ? q.QuestionText.Substring(0, 47) + "..." : q.QuestionText
                ).ToList();

                competency.CompetencyChartLabels = questionNames;

                foreach (var category in categories)
                {
                    var questionSeriesData = new List<double?>();
                    bool hasQuestionData = false;

                    foreach (var question in competency.QuestionScaleReports)
                    {
                        if (question.ScoresByRaterCategory.ContainsKey(category))
                        {
                            questionSeriesData.Add(question.ScoresByRaterCategory[category]);
                            if (question.ScoresByRaterCategory[category].HasValue)
                            {
                                hasQuestionData = true;
                            }
                        }
                        else
                        {
                            questionSeriesData.Add(null);
                        }
                    }

                    if (hasQuestionData)
                    {
                        competency.CompetencyComparisonSeries.Add(new ReportSeriesData
                        {
                            SeriesLabel = category.GetDisplayName(),
                            Data = questionSeriesData,
                            BackgroundColor = category.GetChartColor(),
                            BorderColor = category.GetChartColor()
                        });
                    }
                }
            }
        }
    }
}