namespace ToolBox.Models
{
    public class Feedback360ResponseScale
    {
        public long Id { get; set; }
        
        public long Feedback360RaterId { get; set; }
        public Feedback360Rater? Feedback360Rater { get; set; }
        
        // Identificador de la pregunta predefinida, ej: "LDR_Q1", "COM_Q2"
        public string QuestionCode { get; set; } = string.Empty;
        
        // Puntuación de 1 a 5
        public int Score { get; set; }
    }
}