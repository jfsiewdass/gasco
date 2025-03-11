using Gasco.Common.Entities;
using Gasco.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasco.Repositories.Repositories
{
    public class UserRepo
    {
        private readonly ApplicationDbContext _dataContext;
        internal DbSet<User> dbSet;

        public UserRepo(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
            this.dbSet = _dataContext.Set<User>();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _dataContext.Users.ToListAsync();

            return users;
        }

        public async Task<User?> GerUserByEmail(String Email)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == Email);

            return user;
        }


    }
}
