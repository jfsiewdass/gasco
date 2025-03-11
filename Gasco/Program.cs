using Gasco.AppLogic;
using Gasco.Common.Entities.JWToken;
using Gasco.Common.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace Gasco
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            

            Log.Information("Iniciando Aplicación...");

            var configuration = builder.Configuration;

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.AddRepositories();
            builder.AddServices();

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                var jwtInfo = configuration.GetSection("JWT").Get<JwtInfo>();
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtInfo!.ValidAudience,
                    ValidIssuer = jwtInfo.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo!.SecretKey!)),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

            var app = builder.Build();
            ServiceProviderHelper.Initialize(app.Services);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (builder.Environment.EnvironmentName == "Development")
            {
                Log.Information("Modo desarrollo");

                app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("*"));

                Log.Information($"{app.Configuration.ToString()}");
            }
            else
            {

                Log.Information("Modo producción");

               // app.UseCors(x => x
               //.WithOrigins("https://sare.cne.org.ve", "https://sareapp.cne.org.ve")
               //.WithMethods("POST", "GET", "OPTIONS", "DELETE", "PUT")
               //.WithHeaders("Content-Type", "Authorization")
               //.SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));

                //app.UseMiddleware<OriginRestrictionMiddleware>();

                Log.Information($"{app.Configuration.ToString()}");
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<EvaluateJWTMiddleware>();

            app.Run();
        }
    }
}
