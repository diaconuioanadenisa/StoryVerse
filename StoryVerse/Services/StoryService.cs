using StoryVerse.Models;
using StoryVerse.Repositories;
using System.Collections.Generic;

namespace StoryVerse.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _repository;

        public StoryService(IStoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Story> GetStories() => _repository.GetAll();
        public Story GetStoryById(int id) => _repository.GetById(id);
        public void CreateStory(Story story) => _repository.Add(story);
        public void UpdateStory(Story story) => _repository.Update(story);
        public void DeleteStory(int id) => _repository.Delete(id);
        public IEnumerable<Story> SearchStories(string term) => string.IsNullOrWhiteSpace(term) ? _repository.GetAll() : _repository.Search(term);

        // Chemăm repository-ul pentru autori
        public IEnumerable<User> GetAuthors() => _repository.GetAllUsers();
    }
}