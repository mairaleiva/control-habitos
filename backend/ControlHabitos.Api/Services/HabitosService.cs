using ControlHabitos.Data;
using ControlHabitos.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace  ControlHabitos.Api.Services
{
    public class HabitosService
    {
        private readonly AppDbContext _context;
        public HabitosService (AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<List<Habito>> ObtenerHabitos()
        {
            var habitos = await this._context.Habitos.ToListAsync();

            return habitos;
        }

        public async Task<Habito> CrearHabitos(Habito habito)
        {
            await _context.Habitos.AddAsync(habito);
            await _context.SaveChangesAsync();

            return habito;
        }

        public async Task<Habito?> ActualizarHabito(Habito habito, long IdHabito)
        {
            var habitoEncontrado = await _context.Habitos.FirstOrDefaultAsync(x => x.Id == IdHabito);

            if(habitoEncontrado is null)
                return null;

            habitoEncontrado.Nombre = habito.Nombre;
            habitoEncontrado.Completo = habito.Completo;

            await _context.SaveChangesAsync();

            return habitoEncontrado;
        }

        public async Task<bool> EliminarHabito(long IdHabito)
        {
            var habitoEncontrado = await _context.Habitos.FirstOrDefaultAsync(x => x.Id == IdHabito);

            if(habitoEncontrado is null)
                return false;

            _context.Habitos.Remove(habitoEncontrado);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}