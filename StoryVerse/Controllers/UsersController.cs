using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryVerse.Models;
using StoryVerse.Services;
using System.Linq;

namespace StoryVerse.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // --- METODĂ MODIFICATĂ PENTRU REDIRECȚIONARE ---
        [Authorize]
        public IActionResult MyProfile()
        {
            // Dacă cel logat este Admin, îl trimitem instant la tabelul cu utilizatori (Index)
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(nameof(Index));
            }

            var username = User.Identity?.Name;
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Username == username);

            if (user == null) return RedirectToAction("Login", "Auth");

            return View(user);
        }

        // =================================================================
        // PAGINILE DE MAI JOS SUNT DOAR PENTRU ADMIN
        // =================================================================

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_userService.GetAllUsers());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id,Username,Email,Password")] User user)
        {
            if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Email))
            {
                _userService.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id) => View(_userService.GetUserById(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("Id,Username,Email,Password")] User user)
        {
            if (id != user.Id) return NotFound();

            if (!string.IsNullOrEmpty(user.Username))
            {
                _userService.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) => View(_userService.GetUserById(id));

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id) => View(_userService.GetUserById(id));
    }
}