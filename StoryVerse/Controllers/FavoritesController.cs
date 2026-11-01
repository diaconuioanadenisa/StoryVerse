using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryVerse.Data;
using StoryVerse.Models;
using System.Linq;

namespace StoryVerse.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            // 1. Aflăm cine este utilizatorul conectat
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null) return RedirectToAction("Login", "Auth");

            // 2. Extragem din bază doar poveștile care au legătură cu acest utilizator în tabela Favorites
            var favoriteStories = _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Story) // Selectăm povestea propriu-zisă
                .ToList();

            // 3. Trimitem lista de povești către pagina vizuală
            return View(favoriteStories);
        }


        [HttpPost]
        public IActionResult ToggleFavorite(int storyId)
        {
            var username = User.Identity.Name;
            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if (user == null) return RedirectToAction("Login", "Auth");

          
            var existingFavorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == user.Id && f.StoryId == storyId);

            if (existingFavorite != null)
            {
                _context.Favorites.Remove(existingFavorite);
            }
            else
            {
                var newFavorite = new Favorite
                {
                    UserId = user.Id,
                    StoryId = storyId
                };
                _context.Favorites.Add(newFavorite);
            }

            _context.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}