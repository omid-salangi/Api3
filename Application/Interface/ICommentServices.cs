using Domain.Model;

namespace Application.Interface;

public interface ICommentServices
{
    Task Create(Comments comments, CancellationToken cancellationToken);
    Task Delete(int id , CancellationToken cancellationToken);
    Task Update(Comments comments, CancellationToken cancellationToken);
    Task<Comments> Read(int id , CancellationToken cancellationToken);
    Task ConfirmCommnet (int id , CancellationToken cancellationToken);
    Task<IEnumerable<Comments>> GetUnconfirmedComment(CancellationToken cancellationToken);
    Task<IEnumerable<Comments>> GetCommnetsOfAPost(int postid, CancellationToken cancellationToken);

}