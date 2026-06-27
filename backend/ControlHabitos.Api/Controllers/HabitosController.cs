using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;
using ControlHabitos.Api.Services;
using ControlHabitos.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        [Authorize]
        public async Task<ActionResult<List<HabitoResponseDto>>> Habitos()
        {
            var idUsuarioClaim = ObtenerUsuarioId();

            if(idUsuarioClaim is null)
                return Unauthorized("No se pudo identificar al usuario autenticado.");

            var habitos = await _habitosService.ObtenerHabitos(idUsuarioClaim.Value);

            var habitosResponse = habitos.Select(x => MapearHabitoResponse(x));

            return Ok(habitosResponse);
        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<HabitoResponseDto>> Habitos(long Id)
        {
            var idUsuarioClaim = ObtenerUsuarioId();

            if(idUsuarioClaim is null)
                return Unauthorized("No se pudo identificar al usuario autenticado.");

            var habito = await _habitosService.ObtenerHabitoPorId(Id, idUsuarioClaim.Value);
            
            if(habito is null)
                return NotFound("No existe el hábito que intenta consultar.");
                
            var habitoResponse = MapearHabitoResponse(habito);

            return habitoResponse;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<HabitoResponseDto>> nuevoHabito ([FromBody] CrearHabitoDto habitoDto)
        {
            var idUsuarioClaim = ObtenerUsuarioId();

            if(idUsuarioClaim is null)
                return Unauthorized("No se pudo identificar al usuario autenticado.");

            var habito = new Habito
                {
                    Nombre = habitoDto.Nombre,
                    Completo = habitoDto.Completo,
                    IdUsuario = idUsuarioClaim.Value
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
        [Authorize]
        public async Task<ActionResult<HabitoResponseDto>> Actualizar ([FromBody] ActualizarHabitoDto habitoDto, long Id)
        {
            var habitoMapeado = new Habito
            {
                Nombre = habitoDto.Nombre,
                Completo = habitoDto.Completo
            };

            var idUsuarioClaim = ObtenerUsuarioId();

            if(idUsuarioClaim is null)
                return Unauthorized("No se pudo identificar al usuario autenticado.");

            var habitoExistente = await _habitosService.ActualizarHabito(habitoMapeado, Id, idUsuarioClaim.Value);

            if(habitoExistente is null)
                return NotFound("No se encontró el habito que quiere actualizar");

            var habitoResponse = MapearHabitoResponse(habitoExistente);

            return Ok(habitoResponse);
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<ActionResult> Eliminar (long Id)
        {
            var idUsuarioClaim = ObtenerUsuarioId();

            if(idUsuarioClaim is null)
                return Unauthorized("No se pudo identificar al usuario autenticado.");

            var habitoExistente = await _habitosService.EliminarHabito(Id, idUsuarioClaim.Value);

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

        private long? ObtenerUsuarioId()
        {
            return Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}