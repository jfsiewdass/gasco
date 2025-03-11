using Gasco.Common.Entities;
using Gasco.Common.ServerResponses;
using Gasco.Repositories.Repositories;

namespace Gasco.AppLogic
{
    public class UserAppLogic
    {
        private readonly UserRepo _userRepo;

        public UserAppLogic(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<JSendResponse<List<User>>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();

            return new JSendResponse<List<User>> { Data = users };
        }
    }
}
