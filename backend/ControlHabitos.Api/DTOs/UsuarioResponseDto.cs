using System.ComponentModel.DataAnnotations;

namespace ControlHabitos.Api.DTOs
{
    public class UsuarioResponseDto
    {
        public long Id {set; get;}
        public string Nombre {set; get;} = string.Empty;
        public string Email {set; get;} = string.Empty;
    }
}