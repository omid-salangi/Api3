using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Interface
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(string id, CancellationToken cancellationToken);
        public Task<bool> Any(string username , CancellationToken cancellationToken);
        
        public Task UpdateLastLogin(string id, CancellationToken cancellationToken);
    }
}
