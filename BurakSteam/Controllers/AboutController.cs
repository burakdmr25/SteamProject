using Microsoft.AspNetCore.Mvc;

namespace BurakSteam.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
