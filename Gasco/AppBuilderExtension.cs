using Gasco.Repositories.Context;
using Gasco.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Gasco
{
    public static class AppBuilderExtension
    {

        internal static void AddRepositories(this WebApplicationBuilder builder)
        {
            //builder.AddOptions();
            builder.AddMappers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Gasco")));
            builder.Services.AddScoped<UserRepo>();
        }

        internal static void AddConfigurationFiles(this WebApplicationBuilder builder)
        {
            if (builder.Environment.EnvironmentName == "Development")
                builder.Configuration.AddJsonFile("appsettings.Development.json");

            if (builder.Environment.EnvironmentName == "Production")
                builder.Configuration.AddJsonFile("appsettings.Production.json");

            if (File.Exists("DocumentsInformationSettings.json"))
                builder.Configuration.AddJsonFile("DocumentsInformationSettings.json");

        }
        private static void AddMappers(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}

