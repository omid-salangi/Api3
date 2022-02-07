using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Model;

namespace Data.Repositories
{

    public class PostRepository : IPostRepository
    {
        private readonly Context _context;

        public PostRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> AddPost(string title, string description,DateTime time,string author , CancellationToken cancellationToken)
        {
            Post post = new Post()
            {
                UserId = 1,
                title = title,
                Description = description,
                author = author,
                Time = time

            };
         var res=   await _context.Posts.AddAsync(post,cancellationToken);
          var res1= await _context.SaveChangesAsync(cancellationToken);
           return true;
        }

        public async Task<bool> DeletePost(int postid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePost(int postid, string title, string description, int[] category, DateTime time)
        {
            throw new NotImplementedException();
        }
    }
}
