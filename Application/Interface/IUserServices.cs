using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Application.Interface
{
    public interface IUserServices
    {
       // public Task<bool> AddUserAsync (string username, string fullname, string password, int age,string email, CancellationToken cancellationToken);
        public Task<bool> Any(string username, CancellationToken cancellationToken);
     //   public Task<User> GetByUsername(string username , string password , CancellationToken cancellationToken);
        public  Task<User> GetUserById(CancellationToken cancellationToken,string id);
        public Task UpdateLastLogin(string id, CancellationToken cancellationToken);
    }
}
