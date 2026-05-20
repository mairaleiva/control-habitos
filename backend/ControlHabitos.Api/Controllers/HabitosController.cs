using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;

namespace ControlHabitos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitosController : ControllerBase
    {
        private static List<Habito> ListHabito = new List<Habito>();

        [HttpGet]
        public ActionResult<List<Habito>> Habitos()
        {
            var habitos = ListHabito;

            return Ok(habitos);
        }

        [HttpPost]
        public ActionResult<Habito> nuevoHabito ([FromBody] Habito habito)
        {
            ListHabito.Add(habito);

            return Ok(habito);
        }

        [HttpPut("{Id}")]
        public ActionResult<Habito> Actualizar ([FromBody] Habito habito, long Id)
        {
            var habitoExistente = ListHabito.FirstOrDefault(x => x.Id == Id);

            if(habitoExistente is null)
                return NotFound("El hábito que quiere Editar no existe.");

            //Actualizo
            habitoExistente.Nombre = habito.Nombre;
            habitoExistente.Completo = habito.Completo;
            
            return Ok(habitoExistente);
        }

    }
}