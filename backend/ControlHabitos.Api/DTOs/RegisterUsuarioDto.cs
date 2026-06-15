using System.ComponentModel.DataAnnotations;

namespace ControlHabitos.Api.DTOs
{
    public class RegisterUsuarioDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [MaxLength(50)]
        public string Nombre {set; get;} = string.Empty;
        
        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del Email no es válido")]
        public string Email {set; get;} = string.Empty;
        
        [Required(ErrorMessage = "La Password es obligatorio")]
        [StringLength(100, MinimumLength = 6)]
        public string Password {set; get;} = string.Empty;
    }
}