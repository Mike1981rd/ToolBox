namespace ToolBox.Models
{
    public enum QuestionType
    {
        SingleChoice,      // Opción Múltiple - Respuesta Única
        MultipleChoice,    // Opción Múltiple - Respuesta Múltiple
        ShortText,         // Respuesta Corta (input type text)
        LongText,          // Respuesta Larga (textarea)
        LikertScale,       // Escala Likert (ej: 1-5)
        TrueFalse          // Verdadero/Falso
    }
}