using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Gasco.AppLogic
{
    public static class AppBuilderExtension
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            
            builder.Services.AddScoped<UserAppLogic>();
            builder.Services.AddScoped<JwtAppLogic>();
            builder.Services.AddScoped<AuthAppLogic>();
        }
    }
}
