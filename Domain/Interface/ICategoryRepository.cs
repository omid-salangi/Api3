using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories(CancellationToken cancellationToken);
        Task AddCategory(Category category , CancellationToken cancellationToken);
        Task DeleteCategory(int id , CancellationToken cancellationToken);
        Task EditCategory(Category category , CancellationToken cancellationToken);
        Task<Category> GetCategory(int id);
    }
}
