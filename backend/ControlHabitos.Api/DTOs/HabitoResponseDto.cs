namespace ControlHabitos.Api.DTOs
{
    public class HabitoResponseDto
    {
        public long Id {set; get;}
        public string Nombre {set; get;} = string.Empty;
        public bool Completo {set; get;}
    }
}