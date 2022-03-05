using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dependency;
using Domain.Model;

namespace Data.Repositories
{
    public class CommentRepository : ICommentRepository , IScopedDependency
    { 
        private readonly ApplicationContext _context;

        public CommentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task Add(string userid,int postid, string comment, CancellationToken cancellationToken)
        {
            Comments temp = new Comments()
            {
                Comment = comment,
                SubmitTime = DateTimeOffset.Now,
                UserId = userid,
                PostId = postid
            };
            _context.Comments.AddAsync(temp);
            _context.SaveChangesAsync();
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var temp =await _context.Comments.FindAsync(id);
            _context.Remove(temp);
          await  _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Confirm(int id, CancellationToken cancellationToken)
        {
            var temp =await _context.Comments.FindAsync(id);
            temp.IsConfirmed=true;
           await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UnConfirm(int id, CancellationToken cancellationToken)
        {
            var temp = await _context.Comments.FindAsync(id, cancellationToken);
            temp.IsConfirmed=false;
          await  _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(int id, string comment, CancellationToken cancellationToken)
        {
            var temp = await _context.Comments.FindAsync(id,cancellationToken);
            temp.Comment = comment;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
