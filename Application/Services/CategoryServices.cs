using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Common.Dependency;
using Common.Exceptions;
using Domain.Interface;
using Domain.Model;

namespace Application.Services
{
    public class CategoryServices : ICategoryServices , IScopedDependency
    {
        private readonly ICategoryRepository _category;

        public CategoryServices(ICategoryRepository category)
        {
            _category = category;
        }
       

        public async Task Create(Category category, CancellationToken cancellationToken)
        {
            await _category.AddCategory(category, cancellationToken);
        }

        public async Task delete(int id, CancellationToken cancellationToken)
        {
            await _category.DeleteCategory(id, cancellationToken);
        }

        public async Task Update(Category category, CancellationToken cancellationToken)
        {
           await _category.EditCategory(category, cancellationToken);
        }

        public async Task<Category> GetById(int id, CancellationToken cancellationToken)
        {
           var res = await _category.GetCategory(id, cancellationToken);
           if (res == null)
           {
               throw new BadRequestException("دسته بندی مورد نظر یافت نشد.");
           }
           else
           {
               return res;
           }
        }

        public async Task<Category> GetByName(string name, CancellationToken cancellationToken)
        {
            var res =  await _category.GetCategoryByName(name, cancellationToken);
            if (res == null)
            {
                throw new BadRequestException("نام دسته بندی موجود نمی باشد.");
            }
            else
            {
                return res;
            }
        }

        public Task<IEnumerable<Category>> GetAllCategories(CancellationToken cancellationToken)
        {
            return _category.GetAllCategories(cancellationToken);
        }
    }
}
