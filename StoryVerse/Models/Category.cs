using System.Collections.Generic;

namespace StoryVerse.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StoryCategory> StoryCategories { get; set; }
    }
}