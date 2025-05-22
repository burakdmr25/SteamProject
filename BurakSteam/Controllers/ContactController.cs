using Microsoft.AspNetCore.Mvc;
using BurakSteam.Models;
using BurakSteam.Data;
using Microsoft.EntityFrameworkCore;

namespace BurakSteam.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        // ContactController'a ApplicationDbContext'i enjekte ediyoruz
        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // İletişim formunu gösterme (GET)
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ContactForm());
        }

        // İletişim formu gönderildiğinde işleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                // Gönderilen formu kaydediyoruz
                model.SubmittedAt = DateTime.Now; // Mesajın gönderilme tarihini ayarlıyoruz
                _context.ContactForms.Add(model);  // Veritabanına ekliyoruz
                await _context.SaveChangesAsync(); // Veritabanına kaydediyoruz

                // Başarı mesajı
                TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi!";

                // Formu temizleyip, iletişim sayfasına yönlendiriyoruz
                return RedirectToAction("Index");
            }

            // Eğer model geçerli değilse, aynı formu hata mesajlarıyla geri gönderiyoruz
            return View("Index", model);
        }
    }
}
