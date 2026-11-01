using System;
using System.Collections.Generic;

namespace StoryVerse.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int AuthorId { get; set; }

       
        public User? Author { get; set; }

       
        public List<Comment>? Comments { get; set; }
        public List<Favorite>? Favorites { get; set; }
        public List<StoryCategory>? StoryCategories { get; set; }
    }
}