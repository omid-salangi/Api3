using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Model;

namespace Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return _context.Categories;
        }



        public async Task AddCategory(string name, string description)
        {
            Category temp = new Category()
            {
                Name = name,
                Description = description
            };
            await _context.Categories.AddAsync(temp);

            _context.SaveChanges();



        }

        public async Task DeleteCategory(int id)
        {
            Category temp = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(temp);
            _context.SaveChanges();


        }

        public async Task EditCategory(int id, string name, string description)
        {
            Category temp = await _context.Categories.FindAsync(id);
            temp.Name = name;
            temp.Description = description;
            _context.SaveChanges();
        }

        public async Task<Category> GetCategory(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            return category;
        }
    }
}
