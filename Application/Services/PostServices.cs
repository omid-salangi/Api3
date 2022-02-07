using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.ViewModel;
using Domain.Interface;

namespace Application.Services
{
    public class PostServices : IPostService
    {
        private readonly IPostRepository _post;

        public PostServices(IPostRepository post)
        {
            _post = post;
        }

        public async Task<bool> AddPost(AddPostViewModel model,CancellationToken cancellationToken)
        { 
            return await _post.AddPost(model.title, model.description,DateTime.Now , model.author,cancellationToken);
        }
    }
}
