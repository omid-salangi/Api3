using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Application.Model;
using Common.Dependency;
using Domain.Interface;
using Domain.Model;

namespace Application.Services
{
    public class PostServices : IPostService , IScopedDependency
    {
        private readonly IPostRepository _post;

        public PostServices(IPostRepository post)
        {
            _post = post;
        }
        public async Task AddPost(Post post, CancellationToken cancellationToken)
        {
            await _post.AddPost(post, cancellationToken);
        }

        public async Task DeletePost(int postId, CancellationToken cancellationToken)
        {
            await _post.DeletePost(postId, cancellationToken);
        }

        public async Task<IEnumerable<Post>> GetAllPosts(CancellationToken cancellationToken)
        {
            return await _post.GetAllPosts(cancellationToken);
        }

        public Task<Post> GetPost(int postId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePost(Post post, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
