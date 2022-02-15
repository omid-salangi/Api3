using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;

namespace Application.Interface
{
    public interface IPostService
    {
        public Task<bool> AddPost(Postdto model,CancellationToken cancellationToken);
    }
}
