using StoryVerse.Models;
using System.Collections.Generic;

namespace StoryVerse.Services
{
    public interface IStoryService
    {
        IEnumerable<Story> GetStories();
        Story GetStoryById(int id);
        void CreateStory(Story story);
        void UpdateStory(Story story);
        void DeleteStory(int id);
        IEnumerable<Story> SearchStories(string term);
        IEnumerable<User> GetAuthors();
    }
}