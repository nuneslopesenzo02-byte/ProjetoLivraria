using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_Livraria.Models
{
    [Table("livro")]
    public class Livro
    {
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public decimal Valor { get; set; }
    }
}