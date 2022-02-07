using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModel;

namespace Application.Interface
{
    public interface IPostService
    {
        public Task<bool> AddPost(AddPostViewModel model,CancellationToken cancellationToken);
    }
}
