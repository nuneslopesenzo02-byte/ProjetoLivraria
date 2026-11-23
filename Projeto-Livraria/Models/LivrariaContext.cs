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

        public DbSet<Logins> Login { get; set; }

        public DbSet<Livros> Livro { get; set; }
    }
}