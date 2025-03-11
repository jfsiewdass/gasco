using Gasco.Common.Exceptions;
using Gasco.Repositories.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace Gasco
{
    public class EvaluateJWTMiddleware
    {
        private readonly RequestDelegate _next;

        public EvaluateJWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwtToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (jwtToken != null) {

                if (await EvaluateExpiredToken(jwtToken!))
                {
                    throw new GascoException(GascoExceptionCodes.GascoEx1011);
                }
                else {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                    JwtSecurityToken token = handler.ReadJwtToken(jwtToken);

                    string userId = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value!;

                    using (var scope = context.RequestServices.CreateScope())
                    {
                        var usersRepo = scope.ServiceProvider.GetRequiredService<UserRepo>();
                        //var user = await usersRepo.GetUserAsync(int.Parse(userId));

                        //if(user!.Token != jwtToken && user.RolId != (int)Rol.ApiAuth) throw new SareException(SareExceptionCodes.SareEx9999);
                    }
                }
            }

            await _next(context);
        }

        public async Task<bool> EvaluateExpiredToken(string token)
        {
            if (token == null) return false;

            var handler = new JwtSecurityTokenHandler();

            try
            {
                var tokenS = handler.ReadJwtToken(token);

                var expiration = tokenS.ValidTo;

                return expiration < DateTime.UtcNow;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
