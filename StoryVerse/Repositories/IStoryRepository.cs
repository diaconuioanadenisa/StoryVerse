using StoryVerse.Models;

namespace StoryVerse.Repositories
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetAll();
        Story GetById(int id);
        void Add(Story story);
        void Update(Story story);
        void Delete(int id);
    
    IEnumerable<Story> Search(string term);
    IEnumerable<User> GetAllUsers();
    }
}
