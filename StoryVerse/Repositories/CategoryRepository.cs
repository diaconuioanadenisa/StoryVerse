using StoryVerse.Data;
using StoryVerse.Models;
using System.Collections.Generic;
using System.Linq;

namespace StoryVerse.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll() => _context.Categories.ToList();
        public Category GetById(int id) => _context.Categories.Find(id);
        public void Add(Category category) { _context.Categories.Add(category); _context.SaveChanges(); }
        public void Update(Category category) { _context.Categories.Update(category); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat != null) { _context.Categories.Remove(cat); _context.SaveChanges(); }
        }
    }
}