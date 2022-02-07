using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using System.Security.Claims;

namespace Application.Interface
{
    public interface IJwtServices
    {
        Task<string> Generate(User user);
        Task<IEnumerable<Claim>> _GetClaims(User user);
    }
}
