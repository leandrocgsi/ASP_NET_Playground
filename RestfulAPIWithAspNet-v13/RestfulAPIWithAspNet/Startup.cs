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
using UpBrasil.OTP.API.Utils;
using RestfulAPIWithAspNet.Conveters;
using RestfulAPIWithAspNet.Models.Entities;
using RestfulAPIWithAspNet.Data.VO;
using RestfulAPIWithAspNet.Business;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using RestfulAPIWithAspNet.Filters;

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
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options =>
                options.UseMySql(connection)
            );

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddScoped<LanguageActionFilter>();

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("pt-BR"),
                        new CultureInfo("en-US"),
                        new CultureInfo("es-ES"),
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddScoped<BookBusiness>();
            services.AddScoped<ContactBusiness>();
            services.AddScoped<FilmBusiness>();

            //services.AddScoped<IRepository<Book>, GenericRepository<Book>>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, MySQLContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //SEE: https://docs.microsoft.com/pt-br/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio
            app.UseSwagger();
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