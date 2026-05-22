using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;
using ControlHabitos.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlHabitos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HabitosController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Habito>>> Habitos()
        {
            var habitos = await _context.Habitos.ToListAsync();

            return Ok(habitos);
        }

        [HttpPost]
        public async Task<ActionResult<Habito>> nuevoHabito ([FromBody] Habito habito)
        {
            await _context.AddAsync(habito);

            await _context.SaveChangesAsync();

            return Ok(habito);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Habito>> Actualizar ([FromBody] Habito habito, long Id)
        {
            var habitoExistente = await _context.Habitos.FirstOrDefaultAsync(x => x.Id == Id);

            if(habitoExistente is null)
                return NotFound("El hábito que quiere Editar no existe.");

            //Actualizo
            habitoExistente.Nombre = habito.Nombre;
            habitoExistente.Completo = habito.Completo;

            await _context.SaveChangesAsync();
            
            return Ok(habitoExistente);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Eliminar (long Id)
        {
            var habitoExistente = await _context.Habitos.FirstOrDefaultAsync(x => x.Id == Id);

            if(habitoExistente is null)
                return NotFound("El hábito que quiere Eliminar no existe.");

            _context.Habitos.Remove(habitoExistente);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}