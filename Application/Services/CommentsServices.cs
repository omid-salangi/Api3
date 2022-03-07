using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Common.Dependency;
using Domain.Interface;
using Domain.Model;

namespace Application.Services
{
    public class CommentsServices : ICommentServices , IScopedDependency
    {
        private readonly ICommentRepository _comment;

        public CommentsServices(ICommentRepository comment)
        {
            _comment = comment;
        }
        public  async Task Create(Comments comments, CancellationToken cancellationToken)
        {
            await _comment.Add(comments.UserId, comments.PostId, comments.Comment, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _comment.Delete(id, cancellationToken);
        }

        public async Task Update(Comments comments, CancellationToken cancellationToken)
        {
            await _comment.Update(comments.Id, comments.Comment, cancellationToken);
        }

        public async Task<Comments> Read(int id, CancellationToken cancellationToken)
        {
            return await _comment.GetCommnetById(id, cancellationToken);
        }

        public async Task ConfirmCommnet(int id, CancellationToken cancellationToken)
        {
            await _comment.Confirm(id, cancellationToken);
        }

        public async Task<IEnumerable<Comments>> GetUnconfirmedComment(CancellationToken cancellationToken)
        {
            return await _comment.GetUnconfirmedComment(cancellationToken);
        }

        public async Task<IEnumerable<Comments>> GetCommnetsOfAPost(int postid, CancellationToken cancellationToken)
        {
            return await _comment.GetCommnetsOfAPost(postid, cancellationToken);
        }
    }
}
