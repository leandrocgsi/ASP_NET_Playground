using Microsoft.EntityFrameworkCore;

namespace restful_api_with_aspnet.Models
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
