using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SkyHorizon_2223262.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SkyHorizon_2223262.Controllers
{
    public class AdminController : Controller
    {
        private readonly SkyHorizonContext _context;

        public AdminController(SkyHorizonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Dashboard");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Verificar se é admin
                if (user.Role != "Admin")
                {
                    ViewBag.Error = "Acesso negado. Esta área é apenas para administradores.";
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Utilizador ou palavra-passe incorretos";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalDestinations = await _context.Destinations.CountAsync();
            ViewBag.TotalBookings = await _context.Bookings.CountAsync();
            ViewBag.TotalClientes = await _context.Users.CountAsync(u => u.Role == "User");
            return View();
        }

        // Gestão de Destinos
        [Authorize]
        public async Task<IActionResult> Destinos()
        {
            var destinos = await _context.Destinations.ToListAsync();
            return View(destinos);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CriarDestino()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CriarDestino(Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Destinos));
            }
            return View(destination);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditarDestino(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null) return NotFound();
            return View(destination);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarDestino(Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Update(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Destinos));
            }
            return View(destination);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EliminarDestino(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null)
            {
                _context.Destinations.Remove(destination);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Destinos));
        }

        // Gestão de Reservas
        [Authorize]
        public async Task<IActionResult> Reservas()
        {
            var reservas = await _context.Bookings
                .Include(b => b.Destination)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();
            return View(reservas);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EliminarReserva(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Reservas));
        }

        // Gestão de Clientes
        [Authorize]
        public async Task<IActionResult> Clientes()
        {
            var clientes = await _context.Users
                .Where(u => u.Role == "User")
                .OrderBy(u => u.Username)
                .ToListAsync();
            return View(clientes);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditarCliente(int id)
        {
            var cliente = await _context.Users.FindAsync(id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditarCliente(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Clientes));
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var cliente = await _context.Users.FindAsync(id);
            if (cliente != null && cliente.Role != "Admin")
            {
                _context.Users.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Clientes));
        }
    }
}
