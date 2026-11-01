using System.Collections.Generic;
using System.Xml.Linq;

namespace StoryVerse.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } = "User";

        public List<Story>? Stories { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Favorite>? Favorites { get; set; }
    }
}