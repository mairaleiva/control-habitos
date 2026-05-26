using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;
using ControlHabitos.Api.Services;

namespace ControlHabitos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitosController : ControllerBase
    {
        private readonly HabitosService _habitosService;

        public HabitosController(HabitosService habitosService)
        {
            this._habitosService = habitosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Habito>>> Habitos()
        {
            var habitos = await _habitosService.ObtenerHabitos();

            return Ok(habitos);
        }

        [HttpPost]
        public async Task<ActionResult<Habito>> nuevoHabito ([FromBody] Habito habito)
        {
            await _habitosService.CrearHabitos(habito);

            return Ok(habito);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Habito>> Actualizar ([FromBody] Habito habito, long Id)
        {
            var habitoExistente = await _habitosService.ActualizarHabito(habito, Id);

            if(habitoExistente is null)
                return NotFound("No se encontró el habito que quiere actualizar");

            return Ok(habitoExistente);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Eliminar (long Id)
        {
            var habitoExistente = await _habitosService.EliminarHabito(Id);

            if(habitoExistente)
                return NotFound("No se encontró el habito que quiere eliminar");
                
            return Ok();
        }

    }
}