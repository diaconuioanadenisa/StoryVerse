using StoryVerse.Models;

namespace StoryVerse.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        User Register(string username, string email, string password);
    }
}