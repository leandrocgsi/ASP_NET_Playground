using System;

namespace restful_api_with_aspnet.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Autor { get; set; }
        public decimal Preco { get; set; }
        public DateTime Lancamento { get; set; } = DateTime.UtcNow;
    }
}
