using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Interface
{
    public interface ICommentRepository
    {
        Task Add(string userid, int postid, string comment ,CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Confirm(int id, CancellationToken cancellationToken);
        Task UnConfirm(int id, CancellationToken cancellationToken);
        Task Update(int id, string comment, CancellationToken cancellationToken);
    }
}
