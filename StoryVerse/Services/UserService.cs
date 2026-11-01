using StoryVerse.Models;
using StoryVerse.Repositories;
using System.Collections.Generic;

namespace StoryVerse.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetAllUsers() => _repository.GetAll();
        public User GetUserById(int id) => _repository.GetById(id);
        public void CreateUser(User user) => _repository.Add(user);
        public void UpdateUser(User user) => _repository.Update(user);
        public void DeleteUser(int id) => _repository.Delete(id);
    }
}