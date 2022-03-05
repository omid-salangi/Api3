using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dependency;
using Domain.Interface;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PostRepository : IPostRepository , IScopedDependency
    {
        private readonly ApplicationContext _context;

        public PostRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddPost(Post post, CancellationToken cancellationToken)
        {
            await _context.AddAsync(post, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePost(int postid, CancellationToken cancellationToken)
        {
            var temp = await _context.Posts.FindAsync(postid , cancellationToken);
            _context.Posts.Remove(temp);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdatePost(Post post, CancellationToken cancellationToken)
        {
             _context.Posts.Update(post);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task<Post> ReadPost(int id, CancellationToken cancellationToken)
        {
            return await _context.Posts.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Post>> GetAllPosts(CancellationToken cancellationToken)
        {
            return await _context.Posts.ToListAsync(cancellationToken);
        }
    }
}
