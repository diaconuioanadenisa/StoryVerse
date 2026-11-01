using System.Linq;
using StoryVerse.Data;
using StoryVerse.Models;

namespace StoryVerse.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            
            return _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }

        public User Register(string username, string email, string password)
        {
            
            if (_context.Users.Any(u => u.Username == username))
            {
                return null;
            }

            var newUser = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Role = username.ToLower() == "admin" ? "Admin" : "User"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}