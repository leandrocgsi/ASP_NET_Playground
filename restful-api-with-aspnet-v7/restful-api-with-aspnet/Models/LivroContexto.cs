using Microsoft.EntityFrameworkCore;

namespace restful_api_with_aspnet.Models
{
    public class LivroContexto : DbContext
    {
        public LivroContexto(DbContextOptions<LivroContexto> options) : base(options)
        {
        }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Film> Films { get; set; }
    }
}
