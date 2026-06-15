using System.ComponentModel.DataAnnotations;

namespace ControlHabitos.Api.DTOs
{
    public class ActualizarHabitoDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [MaxLength(50)]
        public string Nombre {set; get;} = string.Empty;
        public bool Completo {set; get;}
    }
}