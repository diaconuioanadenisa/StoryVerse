using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoryVerse.Models;
using StoryVerse.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace StoryVerse.Controllers
{
    public class StoriesController : Controller
    {
        private readonly IStoryService _storyService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService; // Avem nevoie de el pentru a găsi un autor pt comentariu

        public StoriesController(IStoryService storyService, ICommentService commentService, IUserService userService)
        {
            _storyService = storyService;
            _commentService = commentService;
            _userService = userService;
        }

        // Afișează lista de povești
        public IActionResult Index(string searchString)
        {
            var stories = _storyService.SearchStories(searchString);
            ViewData["CurrentFilter"] = searchString;
            return View(stories);
        }
        [Authorize]
        [HttpGet]
        // GET: Stories/Create
        public IActionResult Create()
        {
            var authors = _storyService.GetAuthors();
            ViewBag.AuthorId = new SelectList(authors, "Id", "Username");
            return View();
        }

        // POST: Stories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Content,CreatedAt,AuthorId")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.CreatedAt = DateTime.Now;
                _storyService.CreateStory(story);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthorId = new SelectList(_storyService.GetAuthors(), "Id", "Username", story.AuthorId);
            return View(story);
        }

        // Afișează povestea și comentariile ei
        public IActionResult Details(int id)
        {
            var story = _storyService.GetStoryById(id);
            if (story == null) return NotFound();

            // Încărcăm comentariile în ViewBag
            ViewBag.Comments = _commentService.GetCommentsForStory(id);

            return View(story);
        }

        // Metoda nouă pentru adăugarea comentariului
        [HttpPost]
        public IActionResult AddComment(int storyId, string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                // Căutăm primul utilizator (Emily) ca să fie autorul comentariului
                var user = _userService.GetAllUsers().FirstOrDefault();

                var newComment = new Comment
                {
                    Content = content,
                    StoryId = storyId,
                    UserId = user?.Id ?? 1 // Folosește ID-ul lui Emily sau 1 ca default
                };

                _commentService.AddComment(newComment);
            }

            return RedirectToAction("Details", new { id = storyId });
        }

        public IActionResult Edit(int id) => View(_storyService.GetStoryById(id));

        [HttpPost]
        public IActionResult Edit(int id, Story story)
        {
            _storyService.UpdateStory(story);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id) => View(_storyService.GetStoryById(id));

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _storyService.DeleteStory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}