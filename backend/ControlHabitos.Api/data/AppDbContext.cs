using ControlHabitos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ControlHabitos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Habito> Habitos {get; set;}
    }
}