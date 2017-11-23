using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

using RestfulAPIWithAspNet.Repository;

namespace RestfulAPIWithAspNet
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            //services.AddDbContext<ContactsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //using Dependency Injection
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IContactsRepository, ContactsRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
