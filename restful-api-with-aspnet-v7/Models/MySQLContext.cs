using Microsoft.EntityFrameworkCore;

namespace restful_api_with_aspnet.Models
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Film> Films { get; set; }
    }
}
