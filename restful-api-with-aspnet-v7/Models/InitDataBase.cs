using System;
using System.Linq;

namespace restful_api_with_aspnet.Models
{
    public static class InitDataBase
    {
        public static void Initialize(MySQLContext context)
        {
            context.Database.EnsureCreated();

            if (context.Livros.Any())
            {
                return;
            }

            var livros = new Livro[]
            {
              new Livro{Nome="ASP, ADO Banco de dados na web", Autor="Macoratti", Preco=3.99M, Lancamento= DateTime.Now},
              new Livro{Nome="A Cabana", Autor="Willian P. Young", Preco=29.55M, Lancamento=DateTime.Now},
            };
            foreach (Livro p in livros)
            {
                context.Livros.Add(p);
            }
            context.SaveChanges();
        }
    }
}
