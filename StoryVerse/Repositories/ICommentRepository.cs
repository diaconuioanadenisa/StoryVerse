using StoryVerse.Models;

public interface ICommentRepository
{
    IEnumerable<Comment> GetAll();
    void Add(Comment comment);
    void Delete(int id);
}