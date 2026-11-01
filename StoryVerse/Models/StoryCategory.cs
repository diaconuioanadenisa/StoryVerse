namespace StoryVerse.Models
{
    public class StoryCategory
    {
        public int Id { get; set; }

        public int StoryId { get; set; }
        public Story Story { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}