using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;
using Domain.Model;

namespace Application.Interface
{
    public interface IPostService
    {
        public Task AddPost(Post post,CancellationToken cancellationToken);
        public Task UpdatePost(Post post,CancellationToken cancellationToken);
        public Task DeletePost(int postId,CancellationToken cancellationToken);
        public Task<Post> GetPost(int postId,CancellationToken cancellationToken);
        public Task<IEnumerable<Post>> GetAllPosts(CancellationToken cancellationToken);
    }
}
