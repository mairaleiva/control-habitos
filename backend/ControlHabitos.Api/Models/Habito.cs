namespace ControlHabitos.Models
{
    public class Habito
    {
        public long Id {get; set;}
        public long IdUsuario {get; set;}
        public string Nombre {get; set;} = string.Empty;
        public bool Completo {get; set;}
    }
}