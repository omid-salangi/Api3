using Domain.Model;

namespace Application.Interface;

public interface ICategoryServices
{
    Task Create (Category category , CancellationToken cancellationToken);
    Task delete (int id , CancellationToken cancellationToken);
    Task Update (Category category , CancellationToken cancellationToken);
    Task <Category> GetById (int id , CancellationToken cancellationToken);
    Task <Category> GetByName (string name , CancellationToken cancellationToken );
    Task<IEnumerable<Category>> GetAllCategories(CancellationToken cancellationToken);
}