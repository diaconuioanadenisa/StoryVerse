using StoryVerse.Models;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _repo;
    public CommentService(ICommentRepository repo) { _repo = repo; }
    public IEnumerable<Comment> GetCommentsForStory(int storyId) => _repo.GetAll().Where(c => c.StoryId == storyId);
    public void AddComment(Comment comment) => _repo.Add(comment);
}