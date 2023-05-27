using Cdr.Api.Services;
using Cdr.DataAccess;
using Crd.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Cdr.Api.Startup
{
    public static class ServicesInitialisation
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, WebApplicationBuilder builder) {
            RegisterSwagger(services);
            RegisterDatabaseContextDependencies(services);
            RegisterCustomDependencies(services);
            return services;
        }

        private static void RegisterCustomDependencies(IServiceCollection services)
        {
            services
                .AddScoped<ICsvServices, CsvServices>()
                .AddScoped<ICallRecordRepository, CallRecordRepository>()
                .AddScoped<IUploadsServices, UploadsServices>()
                .AddControllers();
        }

        private static void RegisterDatabaseContextDependencies(IServiceCollection services)
        {
            var migrationsAssembly = Path.GetDirectoryName(Assembly.GetAssembly(typeof(CdrDbContext)).Location);
            services.AddDbContext<CdrDbContext>(
                options => { options.UseSqlite($"Data Source={migrationsAssembly}/cdr.db"); }
            );
        }

        /// <summary>
        /// Used to setup Swagger
        /// </summary>
        /// <param name="services">The startup services we wish to configure</param>
        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "DWS Tech Test Call Detail Record (CDR) API.",
                    Description = "An API to support functionality around the CDR business intelligence platform.",
                    Contact = new OpenApiContact
                    {
                        Name = "John",
                        Url = new Uri("https://jwm.xyz")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

    }
}
