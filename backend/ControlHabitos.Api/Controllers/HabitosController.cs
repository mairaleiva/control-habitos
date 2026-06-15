using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;
using ControlHabitos.Api.Services;
using ControlHabitos.Api.DTOs;

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
        public async Task<ActionResult<List<HabitoResponseDto>>> Habitos()
        {
            var habitos = await _habitosService.ObtenerHabitos();

            var habitosResponse = habitos.Select(x => MapearHabitoResponse(x));

            return Ok(habitosResponse);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<HabitoResponseDto>> Habitos(long Id)
        {
            var habito = await _habitosService.ObtenerHabitoPorId(Id);
            
            if(habito is null)
                return NotFound("No existe el hábito que intenta consultar.");
                
            var habitoResponse = MapearHabitoResponse(habito);

            return habitoResponse;
        }

        [HttpPost]
        public async Task<ActionResult<HabitoResponseDto>> nuevoHabito ([FromBody] CrearHabitoDto habitoDto)
        {
            var habito = new Habito
            {
                Nombre = habitoDto.Nombre,
                Completo = habitoDto.Completo
            };

            await _habitosService.CrearHabitos(habito);

            var habitoResponse = MapearHabitoResponse(habito);
            
            return CreatedAtAction(
                    nameof(Habitos),          
                    new { id = habito.Id },      
                    habitoResponse               
            );
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<HabitoResponseDto>> Actualizar ([FromBody] ActualizarHabitoDto habitoDto, long Id)
        {
            var habitoMapeado = new Habito
            {
                Nombre = habitoDto.Nombre,
                Completo = habitoDto.Completo
            };

            var habitoExistente = await _habitosService.ActualizarHabito(habitoMapeado, Id);

            if(habitoExistente is null)
                return NotFound("No se encontró el habito que quiere actualizar");

            var habitoResponse = MapearHabitoResponse(habitoExistente);

            return Ok(habitoResponse);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Eliminar (long Id)
        {
            var habitoExistente = await _habitosService.EliminarHabito(Id);

            if(!habitoExistente)
                return NotFound("No se encontró el habito que quiere eliminar");

            return Ok();
        }

        private HabitoResponseDto MapearHabitoResponse(Habito habito)
        {
            return new HabitoResponseDto
                        {
                            Id = habito.Id,
                            Nombre = habito.Nombre,
                            Completo = habito.Completo
                        };
        }

    }
}