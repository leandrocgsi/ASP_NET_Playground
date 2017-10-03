using System;
using System.Linq;

namespace restful_api_with_aspnet.Models
{
    public static class InitDataBase
    {
        public static void Initialize(MySQLContext context)
        {
            context.Database.EnsureCreated();

            if (context.Books.Any())
            {
                return;
            }

            var books = new Book[]
            {
              new Book{Title="ASP, ADO Banco de dados na web", Author="Macoratti", Price=3.99M, LaunchDate = DateTime.Now},
              new Book{Title="A Cabana", Author="Willian P. Young", Price=29.55M, LaunchDate = DateTime.Now},
            };
            foreach (Book p in books)
            {
                context.Books.Add(p);
            }
            context.SaveChanges();
        }
    }
}
