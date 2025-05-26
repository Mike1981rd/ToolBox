using System.ComponentModel.DataAnnotations;

namespace ToolBox.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El email o username es requerido")]
        [Display(Name = "Email o Username")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; } = false;
    }
}