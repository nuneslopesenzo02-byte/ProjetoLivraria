using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Livraria.Models
{
    [Table("login")]
    public class Login
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string? Senha { get; set; }

        public string SenhaHash { get; set; }
    }
}