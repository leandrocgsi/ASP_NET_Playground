using Microsoft.EntityFrameworkCore;

namespace RestfulAPIWithAspNet.Models
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
