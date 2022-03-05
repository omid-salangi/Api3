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
    public class CategoryRepository : ICategoryRepository , IScopedDependency
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }

        public async Task AddCategory(Category category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category , cancellationToken);
           await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var temp = await _context.Categories.FindAsync(id,cancellationToken);
            _context.Categories.Remove(temp);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task EditCategory(Category category, CancellationToken cancellationToken)
        {
            _context.Categories.Update(category);
            _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
    }
}
