using StoryVerse.Data;
using StoryVerse.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StoryVerse.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly AppDbContext _context;

        public StoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Story> GetAll() => _context.Stories.Include(s => s.Author).ToList();
        public Story GetById(int id) => _context.Stories.Include(s => s.Author).FirstOrDefault(s => s.Id == id);
        public void Add(Story story) { _context.Stories.Add(story); _context.SaveChanges(); }
        public void Update(Story story) { _context.Stories.Update(story); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var s = _context.Stories.Find(id);
            if (s != null) { _context.Stories.Remove(s); _context.SaveChanges(); }
        }
        public IEnumerable<Story> Search(string term) => _context.Stories.Where(s => s.Title.Contains(term) || s.Content.Contains(term)).Include(s => s.Author).ToList();

        public IEnumerable<User> GetAllUsers() => _context.Users.ToList();
    }
}