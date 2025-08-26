using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BlogX.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informa o E-mail.")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }
    }
}