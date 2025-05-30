namespace ToolBox.Models
{
    public enum RaterStatus
    {
        PendingInvitation, // Aún no se ha enviado la invitación formal
        Invited,           // Invitación enviada
        InProgress,        // Ha abierto el enlace, quizás guardó borrador
        Completed          // Ha completado la evaluación
    }
}