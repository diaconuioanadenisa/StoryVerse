namespace StoryVerse.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int StoryId { get; set; }
        public Story Story { get; set; }
    }
}