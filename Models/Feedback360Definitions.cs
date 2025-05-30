using System.Collections.Generic;

namespace ToolBox.Models
{
    public static class Feedback360Definitions
    {
        public static class Competencies
        {
            public const string LEADERSHIP = "LEADERSHIP";
            public const string COMMUNICATION = "COMMUNICATION";
            public const string TEAMWORK = "TEAMWORK";
            public const string PROBLEM_SOLVING = "PROBLEM_SOLVING";
            public const string ADAPTABILITY = "ADAPTABILITY";
        }

        public static class OpenQuestions
        {
            public const string STRENGTHS = "OPEN_Q1_STRENGTHS";
            public const string IMPROVEMENT = "OPEN_Q2_IMPROVEMENT";
        }

        public static List<CompetencyDefinition> GetCompetencies()
        {
            return new List<CompetencyDefinition>
            {
                new CompetencyDefinition
                {
                    Code = Competencies.LEADERSHIP,
                    NameEnglish = "Leadership",
                    NameSpanish = "Liderazgo",
                    Questions = new List<QuestionDefinition>
                    {
                        new QuestionDefinition
                        {
                            Code = "LDR_Q1",
                            TextEnglish = "Inspires and motivates others toward a common goal.",
                            TextSpanish = "Inspira y motiva a otros hacia un objetivo común."
                        },
                        new QuestionDefinition
                        {
                            Code = "LDR_Q2",
                            TextEnglish = "Delegates tasks effectively.",
                            TextSpanish = "Delega tareas eficazmente."
                        },
                        new QuestionDefinition
                        {
                            Code = "LDR_Q3",
                            TextEnglish = "Takes responsibility for team outcomes.",
                            TextSpanish = "Asume responsabilidad por los resultados del equipo."
                        }
                    }
                },
                new CompetencyDefinition
                {
                    Code = Competencies.COMMUNICATION,
                    NameEnglish = "Communication",
                    NameSpanish = "Comunicación",
                    Questions = new List<QuestionDefinition>
                    {
                        new QuestionDefinition
                        {
                            Code = "COM_Q1",
                            TextEnglish = "Expresses ideas clearly and concisely.",
                            TextSpanish = "Se expresa de manera clara y concisa."
                        },
                        new QuestionDefinition
                        {
                            Code = "COM_Q2",
                            TextEnglish = "Actively listens to others' perspectives.",
                            TextSpanish = "Escucha activamente las perspectivas de los demás."
                        },
                        new QuestionDefinition
                        {
                            Code = "COM_Q3",
                            TextEnglish = "Provides constructive feedback.",
                            TextSpanish = "Proporciona retroalimentación constructiva."
                        }
                    }
                },
                new CompetencyDefinition
                {
                    Code = Competencies.TEAMWORK,
                    NameEnglish = "Teamwork",
                    NameSpanish = "Trabajo en Equipo",
                    Questions = new List<QuestionDefinition>
                    {
                        new QuestionDefinition
                        {
                            Code = "TW_Q1",
                            TextEnglish = "Collaborates effectively with team members.",
                            TextSpanish = "Colabora eficazmente con los miembros del equipo."
                        },
                        new QuestionDefinition
                        {
                            Code = "TW_Q2",
                            TextEnglish = "Contributes to a positive team environment.",
                            TextSpanish = "Contribuye a un ambiente de equipo positivo."
                        },
                        new QuestionDefinition
                        {
                            Code = "TW_Q3",
                            TextEnglish = "Shares knowledge and resources with others.",
                            TextSpanish = "Comparte conocimientos y recursos con otros."
                        }
                    }
                },
                new CompetencyDefinition
                {
                    Code = Competencies.PROBLEM_SOLVING,
                    NameEnglish = "Problem Solving",
                    NameSpanish = "Resolución de Problemas",
                    Questions = new List<QuestionDefinition>
                    {
                        new QuestionDefinition
                        {
                            Code = "PS_Q1",
                            TextEnglish = "Analyzes problems from multiple perspectives.",
                            TextSpanish = "Analiza problemas desde múltiples perspectivas."
                        },
                        new QuestionDefinition
                        {
                            Code = "PS_Q2",
                            TextEnglish = "Develops creative and practical solutions.",
                            TextSpanish = "Desarrolla soluciones creativas y prácticas."
                        },
                        new QuestionDefinition
                        {
                            Code = "PS_Q3",
                            TextEnglish = "Makes decisions based on available data.",
                            TextSpanish = "Toma decisiones basadas en datos disponibles."
                        }
                    }
                },
                new CompetencyDefinition
                {
                    Code = Competencies.ADAPTABILITY,
                    NameEnglish = "Adaptability",
                    NameSpanish = "Adaptabilidad",
                    Questions = new List<QuestionDefinition>
                    {
                        new QuestionDefinition
                        {
                            Code = "ADP_Q1",
                            TextEnglish = "Adapts quickly to changing circumstances.",
                            TextSpanish = "Se adapta rápidamente a circunstancias cambiantes."
                        },
                        new QuestionDefinition
                        {
                            Code = "ADP_Q2",
                            TextEnglish = "Remains flexible when facing obstacles.",
                            TextSpanish = "Se mantiene flexible ante los obstáculos."
                        },
                        new QuestionDefinition
                        {
                            Code = "ADP_Q3",
                            TextEnglish = "Embraces new ideas and approaches.",
                            TextSpanish = "Acepta nuevas ideas y enfoques."
                        }
                    }
                }
            };
        }

        public static List<OpenQuestionDefinition> GetOpenQuestions()
        {
            return new List<OpenQuestionDefinition>
            {
                new OpenQuestionDefinition
                {
                    Code = OpenQuestions.STRENGTHS,
                    TextEnglish = "What do you consider to be {SubjectName}'s main strengths?",
                    TextSpanish = "¿Cuáles consideras que son las principales fortalezas de {SubjectName}?"
                },
                new OpenQuestionDefinition
                {
                    Code = OpenQuestions.IMPROVEMENT,
                    TextEnglish = "What key suggestions do you have for {SubjectName} to improve their effectiveness?",
                    TextSpanish = "¿Qué sugerencias clave tienes para que {SubjectName} pueda mejorar su efectividad?"
                }
            };
        }

        public static Dictionary<int, ScaleLabelDefinition> GetLikertScaleLabels()
        {
            return new Dictionary<int, ScaleLabelDefinition>
            {
                { 1, new ScaleLabelDefinition { English = "Needs Significant Development", Spanish = "Necesita Mucho Desarrollo" } },
                { 2, new ScaleLabelDefinition { English = "Needs Development", Spanish = "Necesita Desarrollo" } },
                { 3, new ScaleLabelDefinition { English = "Meets Expectations", Spanish = "Cumple Expectativas" } },
                { 4, new ScaleLabelDefinition { English = "Exceeds Expectations", Spanish = "Supera Expectativas" } },
                { 5, new ScaleLabelDefinition { English = "Outstanding", Spanish = "Sobresaliente" } }
            };
        }
    }

    public class CompetencyDefinition
    {
        public string Code { get; set; } = string.Empty;
        public string NameEnglish { get; set; } = string.Empty;
        public string NameSpanish { get; set; } = string.Empty;
        public List<QuestionDefinition> Questions { get; set; } = new List<QuestionDefinition>();
    }

    public class QuestionDefinition
    {
        public string Code { get; set; } = string.Empty;
        public string TextEnglish { get; set; } = string.Empty;
        public string TextSpanish { get; set; } = string.Empty;
    }

    public class OpenQuestionDefinition
    {
        public string Code { get; set; } = string.Empty;
        public string TextEnglish { get; set; } = string.Empty;
        public string TextSpanish { get; set; } = string.Empty;
    }

    public class ScaleLabelDefinition
    {
        public string English { get; set; } = string.Empty;
        public string Spanish { get; set; } = string.Empty;
    }
}