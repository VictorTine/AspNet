using System.ComponentModel.DataAnnotations;

namespace BlogX.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required]
        [StringLength(40, MinimumLength =3, ErrorMessage ="O Nome deve conter no mínimo 03 caracteres")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
    }
}