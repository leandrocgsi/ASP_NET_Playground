using Microsoft.EntityFrameworkCore;
using RestfulAPIWithAspNet.Models.Entities;

namespace RestfulAPIWithAspNet.Models
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Film> Films { get; set; }
    }
}
