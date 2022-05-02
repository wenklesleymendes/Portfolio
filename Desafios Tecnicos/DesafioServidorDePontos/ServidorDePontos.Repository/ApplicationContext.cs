using Microsoft.EntityFrameworkCore;
using ServidorDePontos.Core.Model;

namespace ServidorDePontos.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<GameResult> GameResult { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}