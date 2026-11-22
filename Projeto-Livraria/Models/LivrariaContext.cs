using Microsoft.EntityFrameworkCore;
using Projeto_Livraria.Models;

namespace Projeto_Livraria.Data
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(
            DbContextOptions<LivrariaContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Login { get; set; }

        public DbSet<Livro> Livro { get; set; }
    }
}