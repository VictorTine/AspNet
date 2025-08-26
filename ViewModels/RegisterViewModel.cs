using System.ComponentModel.DataAnnotations;

namespace BlogX.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O E-mail inserido é inválido")]
        public string Email { get; set; }
    }
}
