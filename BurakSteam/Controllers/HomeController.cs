using Microsoft.AspNetCore.Mvc;
using BurakSteam.Models;

namespace BurakSteam.Data


{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var games = _context.Games.ToList(); // Oyunlar� veritaban�ndan �ek
            return View(games); // View'a g�nder
        }
    }
}
