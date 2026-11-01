using StoryVerse.Models;

public interface ICommentService
{
    IEnumerable<Comment> GetCommentsForStory(int storyId);
    void AddComment(Comment comment);
}