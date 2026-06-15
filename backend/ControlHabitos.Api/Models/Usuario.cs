namespace ControlHabitos.Models
{
    public class Usuario
    {
        public long Id {get; set;}
        public string Nombre {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string PasswordHash {get; set;} = string.Empty;

    }
}