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
        Task<IEnumerable<Category>> GetAllCategories();
        Task AddCategory(string name, string description);
        Task DeleteCategory(int id);
        Task EditCategory(int id, string name, string description);
        Task<Category> GetCategory(int id);
    }
}
