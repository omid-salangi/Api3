using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Interface
{
    public interface IPostRepository
    {
        public Task AddPost(Post post,CancellationToken cancellationToken);
        public Task DeletePost(int postid, CancellationToken cancellationToken);
        public Task UpdatePost(Post post ,CancellationToken cancellationToken );
        public Task<Post> ReadPost(int id , CancellationToken cancellationToken);

        public Task<IEnumerable<Post>> GetAllPosts(CancellationToken cancellationToken);
    }
}