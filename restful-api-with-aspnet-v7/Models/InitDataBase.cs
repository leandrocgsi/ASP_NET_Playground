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

        }
    }
}
