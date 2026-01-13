using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SkyHorizon_2223262.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SkyHorizon_2223262.Controllers
{
    public class HomeController : Controller
    {
        private readonly SkyHorizonContext _context;

        public HomeController(SkyHorizonContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _context.Destinations.ToListAsync();
            return View(destinations);
        }

        public async Task<IActionResult> Destinos()
        {
            var destinations = await _context.Destinations.ToListAsync();
            return View(destinations);
        }

        public async Task<IActionResult> DetalheDestino(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
                return NotFound();
            return View(destination);
        }

        public async Task<IActionResult> MinhasReservas()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Destination)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> FazerReserva(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MinhasReservas));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CancelarReserva(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MinhasReservas));
        }

        // Login de Utilizador Normal
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index");
            }

            ViewBag.Error = "Utilizador ou palavra-passe incorretos";
            return View();
        }

        // Registo de Utilizador Normal
        [HttpGet]
        public IActionResult Registar()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registar(User user)
        {
            // Verificar se o username já existe
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            
            if (existingUser != null)
            {
                ViewBag.Error = "Este nome de utilizador já está em uso.";
                return View(user);
            }

            // Verificar se o email já existe
            var existingEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            
            if (existingEmail != null)
            {
                ViewBag.Error = "Este email já está registado.";
                return View(user);
            }

            // Garantir que o novo utilizador é do tipo "User"
            user.Role = "User";
            
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Erro ao criar conta: " + ex.Message;
                return View(user);
            }

            // Fazer login automático após registo
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
