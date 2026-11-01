using StoryVerse.Models;
using StoryVerse.Repositories;
using System.Collections.Generic;

namespace StoryVerse.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Category> GetAllCategories() => _repository.GetAll();
        public Category GetCategoryById(int id) => _repository.GetById(id);
        public void CreateCategory(Category category) => _repository.Add(category);
        public void UpdateCategory(Category category) => _repository.Update(category);
        public void DeleteCategory(int id) => _repository.Delete(id);
    }
}