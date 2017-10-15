using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

using Swashbuckle.AspNetCore.Swagger;

using RestfulAPIWithAspNet.Repository;
using RestfulAPIWithAspNet.Models;
using Microsoft.AspNetCore.Rewrite;
using RestfulAPIWithAspNet.Repository.Interfaces;

namespace RestfulAPIWithAspNet
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options =>
                options.UseMySql(connection)
            );

            services.AddMvc().AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // services.AddDbContext<MyContext>(options => options.Use (Configuration.GetConnectionString("DefaultConnection")));
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            //services.Add(new ServiceDescriptor(typeof(MyContext), new MyContext(Configuration.GetConnectionString("DefaultConnection"))));

            //using Dependency Injection
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IContactsRepository, ContactsRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MySQLContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //SEE: https://docs.microsoft.com/pt-br/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();

            RedirectToSwaggerPage(app);

            InitDataBase.Initialize(context);
        }

        private static void RedirectToSwaggerPage(IApplicationBuilder app)
        {
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);
        }
    }
}
