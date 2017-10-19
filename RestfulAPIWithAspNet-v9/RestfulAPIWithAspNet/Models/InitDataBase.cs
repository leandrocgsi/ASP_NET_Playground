using System.Linq;

namespace RestfulAPIWithAspNet.Models
{
    public static class InitDataBase
    {
        public static void Initialize(MySQLContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
