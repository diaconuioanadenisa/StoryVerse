using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StoryVerse.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoryVerse.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _authService.Authenticate(username, password);

            if (user == null)
            {
                // Dacă datele sunt greșite, ne întoarcem la pagină
                return View();
            }

            // Dacă datele sunt bune, creăm "buletinul" utilizatorului
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // Aici e rolul adăugat adineauri!
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Îl logăm oficial în aplicație
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Îl trimitem pe pagina principală
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string email, string password)
        {
            var user = _authService.Register(username, email, password);

            if (user == null)
            {
                // Dacă username-ul există deja, ne întoarcem
                return View();
            }

            // După înregistrare cu succes, îl trimitem să se logheze
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            // Ștergem cookie-ul și îl delogăm
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}