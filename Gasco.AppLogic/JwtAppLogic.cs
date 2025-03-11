using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Gasco.Common.Entities.JWToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Gasco.Common.Exceptions;
using Sare.Common.Entities.JWToken;

namespace Gasco.AppLogic
{
    public class JwtAppLogic
    {
        private readonly JwtInfo _jwtInfo;

        public JwtAppLogic(IConfiguration configuration)
        {
            _jwtInfo = configuration.GetSection("JWT").Get<JwtInfo>()!;
        }

        public string GenerateToken(DateTime duration)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtInfo!.SecretKey!));

            var token = new JwtSecurityToken(
                issuer: _jwtInfo.ValidIssuer,
                audience: _jwtInfo.ValidAudience,
                expires: duration,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                notBefore: DateTime.Now
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(DateTime duration)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            var refreshToken = Convert.ToBase64String(randomNumber);
            var expirationTime = duration;

            return new RefreshToken(refreshToken, expirationTime);
        }

        public bool TokenNotExpired(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }
    }
}
