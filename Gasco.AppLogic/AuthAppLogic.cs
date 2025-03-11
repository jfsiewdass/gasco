using Gasco.Common.Entities;
using Gasco.Common.Entities.DTO;
using Gasco.Common.Entities.JWToken;
using Gasco.Common.Exceptions;
using Gasco.Common.ServerResponses;
using Gasco.Repositories.Repositories;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Gasco.AppLogic
{
    public class AuthAppLogic
    {
        private readonly UserRepo _userRepo;
        private readonly JwtAppLogic _jwtAppLogic;
        private readonly JwtInfo _jwtInfo;
        public AuthAppLogic(
            UserRepo userRepo,
            JwtAppLogic jwtAppLogic,
            IConfiguration configuration
            )
        {
            _userRepo = userRepo;
            _jwtAppLogic = jwtAppLogic;
            _jwtInfo = configuration.GetSection("JWT").Get<JwtInfo>()!;
        }
        public async Task<JSendResponse<LoginInfo>> Login(LoginDto login)
        {
            var user = await _userRepo.GerUserByEmail(login.Username!);
            if (user == null) throw new GascoException(GascoExceptionCodes.GascoEx1001);

            //if (user?.RolId == null) throw new SareException(SareExceptionCodes.SareEx1085);

            if (login.Password != user.Password) throw new GascoException(GascoExceptionCodes.GascoEx1001);

            //if (DateTime.Now >= user.PasswordExpiryTime && user.Status == StatusCatalog.Enabled) throw new SareException(SareExceptionCodes.SareEx1100);

            var token = _jwtAppLogic.GenerateToken(DateTime.Now.AddMinutes(_jwtInfo!.Duration));
            var refreshToken = _jwtAppLogic.GenerateRefreshToken(DateTime.Now.AddMinutes(_jwtInfo!.Refresh));
            LoginInfo loginInfo = new()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name ?? string.Empty,
                Token = token,
                RefreshToken = refreshToken.Token
            };
            
            return new JSendResponse<LoginInfo> { Data = loginInfo };
        }

       
    }
}
