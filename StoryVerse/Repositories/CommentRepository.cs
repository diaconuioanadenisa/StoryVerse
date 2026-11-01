using StoryVerse.Data;
using StoryVerse.Models;
using Microsoft.EntityFrameworkCore;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;
    public CommentRepository(AppDbContext context) { _context = context; }
    public IEnumerable<Comment> GetAll() => _context.Comments.Include(c => c.User).Include(c => c.Story).ToList();
    public void Add(Comment comment) { _context.Comments.Add(comment); _context.SaveChanges(); }
    public void Delete(int id) { var c = _context.Comments.Find(id); if (c != null) { _context.Comments.Remove(c); _context.SaveChanges(); } }
}