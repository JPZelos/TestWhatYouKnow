using System.Collections.Generic;
using System.Linq;
using TWYK.Core.Data;
using TWYK.Core.Domain;

namespace TWYK.Services.Categories
{
    public interface ICategoryService
    {
        IList<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public IList<Category> GetAllCategories() {
            return _categoryRepository.Table.ToList();
        }

        public Category GetCategoryById(int categoryId) {
            if (categoryId == 0) {
                return null;
            }

            return _categoryRepository.GetById(categoryId);
        }
    }
}