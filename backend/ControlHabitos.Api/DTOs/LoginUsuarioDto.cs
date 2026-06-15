using System.ComponentModel.DataAnnotations;

namespace ControlHabitos.Api.DTOs
{
    public class LoginUsuarioDto
    {
        public string Email {set; get;} = string.Empty;

        public string Password {set; get;}= string.Empty;
    }
}