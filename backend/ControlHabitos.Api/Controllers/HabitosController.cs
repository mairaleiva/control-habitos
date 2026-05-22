using Microsoft.AspNetCore.Mvc;
using ControlHabitos.Models;
using ControlHabitos.Data;

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
        public ActionResult<List<Habito>> Habitos()
        {
            var habitos = _context.Habitos.ToList();

            return Ok(habitos);
        }

        [HttpPost]
        public ActionResult<Habito> nuevoHabito ([FromBody] Habito habito)
        {
            _context.Add(habito);

            _context.SaveChanges();

            return Ok(habito);
        }

        [HttpPut("{Id}")]
        public ActionResult<Habito> Actualizar ([FromBody] Habito habito, long Id)
        {
            var habitoExistente = _context.Habitos.FirstOrDefault(x => x.Id == Id);

            if(habitoExistente is null)
                return NotFound("El hábito que quiere Editar no existe.");

            //Actualizo
            habitoExistente.Nombre = habito.Nombre;
            habitoExistente.Completo = habito.Completo;

            _context.SaveChanges();
            
            return Ok(habitoExistente);
        }

        [HttpDelete("{Id}")]
        public ActionResult Eliminar (long Id)
        {
            var habitoExistente = _context.Habitos.FirstOrDefault(x => x.Id == Id);

            if(habitoExistente is null)
                return NotFound("El hábito que quiere Eliminar no existe.");

            _context.Habitos.Remove(habitoExistente);

            _context.SaveChanges();

            return Ok();
        }

    }
}