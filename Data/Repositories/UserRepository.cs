using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Common.Dependency;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository , IScopedDependency
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;

        }
        public async Task<bool> Any(string username , CancellationToken cancellationToken)
        {
            var res = await _context.Users.Where(u => u.NormalizedUserName == username.ToUpper()).ToListAsync(cancellationToken);
            if (res.Count == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Domain.Model.User> GetUserById(string id, CancellationToken cancellationToken)
        {
          return await _context.User.FindAsync(id);
        }

        public async Task UpdateLastLogin(string id, CancellationToken cancellationToken)
        {
           var temp= await _context.User.FindAsync(id);
           temp.LastLoginDate = DateTimeOffset.Now;
           await _context.SaveChangesAsync();
        }
    }
}
