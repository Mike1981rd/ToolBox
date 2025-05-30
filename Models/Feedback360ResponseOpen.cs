namespace ToolBox.Models
{
    public class Feedback360ResponseOpen
    {
        public long Id { get; set; }
        
        public long Feedback360RaterId { get; set; }
        public Feedback360Rater? Feedback360Rater { get; set; }
        
        // Identificador de la pregunta abierta, ej: "OPEN_Q1_STRENGTHS"
        public string QuestionCode { get; set; } = string.Empty;
        
        // Respuesta de texto (TEXT en DB)
        public string ResponseText { get; set; } = string.Empty;
    }
}