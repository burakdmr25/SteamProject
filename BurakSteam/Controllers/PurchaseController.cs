using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurakSteam.Data;
using BurakSteam.Models;

namespace BurakSteam.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Sepet görüntüleme (Checkout)
        public async Task<IActionResult> Checkout()
        {
            var userId = User.Identity?.Name;

            var purchases = await _context.Purchases
                .Include(p => p.Game)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var totalPrice = purchases.Sum(p => p.Price);
            ViewBag.TotalPrice = totalPrice;  // Toplam fiyatı ViewBag üzerinden gönder

            return View(purchases);  // Sepetteki oyunları ve toplam fiyatı görüntülemek için View'a aktar
        }

        // Sepet ile ödeme işlemine geçiş
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProceedToPayment()
        {
            var userId = User.Identity?.Name;

            // Kullanıcının sepetindeki tüm satın alımları alıyoruz
            var purchases = await _context.Purchases
                .Where(p => p.UserId == userId)
                .ToListAsync();

            // Sepetteki toplam fiyatı hesaplıyoruz
            var totalPrice = purchases.Sum(p => p.Price);

            // Eğer toplam fiyat 0 ise, hatalı bir durum olduğunu kontrol et
            if (totalPrice <= 0)
            {
                TempData["ErrorMessage"] = "Sepetinizde ürün bulunmamaktadır.";
                return RedirectToAction("Checkout", "Purchase");
            }

            // Ödeme için viewModel oluşturuyoruz
            var paymentModel = new PaymentViewModel
            {
                Amount = totalPrice
            };

            // Payment (CompletePayment) sayfasına yönlendiriyoruz
            return View("CompletePayment", paymentModel);  // Model'i gönderiyoruz
        }

        // Ödeme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletePayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Ödeme simülasyonu (gerçek ödeme sağlayıcıları entegre edilebilir)
                var paymentSuccess = SimulatePayment(model.CardNumber, model.ExpiryDate, model.Cvc, model.Amount);

                if (paymentSuccess)
                {
                    // Ödeme başarılı ise, sepetteki ürünleri satın alınmış olarak işaretle
                    var userId = User.Identity?.Name;
                    var purchases = await _context.Purchases
                        .Where(p => p.UserId == userId)
                        .ToListAsync();

                    foreach (var purchase in purchases)
                    {
                        purchase.PurchaseDate = DateTime.Now;  // Satın alma tarihini güncelle
                    }

                    _context.UpdateRange(purchases);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Satın alma işleminiz başarıyla tamamlanmıştır.";
                    return RedirectToAction("Checkout", "Purchase");  // Kullanıcıyı Checkout sayfasına yönlendir
                }
                else
                {
                    TempData["ErrorMessage"] = "Ödeme işlemi başarısız oldu. Lütfen kart bilgilerinizi kontrol edin.";
                    return RedirectToAction("Checkout", "Purchase");  // Hata durumunda Checkout sayfasına yönlendir
                }
            }

            // Eğer model geçersizse, ödeme sayfasını tekrar göster
            return View(model);
        }

        // Ödeme simülasyonu
        private bool SimulatePayment(string cardNumber, string expiryDate, string cvc, decimal amount)
        {
            if (cardNumber.Length == 16 && expiryDate.Length == 5 && cvc.Length == 3 && amount > 0)
            {
                return true;  // Ödeme başarılı
            }

            return false;  // Hatalı ödeme bilgisi
        }

        // Satın alma işlemi için oyun ekleme
        public async Task<IActionResult> AddToCart(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);

            if (game == null)
            {
                return NotFound();
            }

            var userId = User.Identity?.Name;

            // Oyun daha önce sepete eklenmiş mi kontrol et
            var existingPurchase = await _context.Purchases
                .FirstOrDefaultAsync(p => p.GameId == gameId && p.UserId == userId);

            if (existingPurchase != null)
            {
                TempData["ErrorMessage"] = "Bu oyun zaten sepete eklenmiş.";
                return RedirectToAction(nameof(Checkout));
            }

            var purchase = new Purchase
            {
                GameId = game.Id,
                UserId = userId,
                Price = (int)game.Price,  // Oyun fiyatı
                PurchaseDate = null // Satın alma tarihi henüz belirlenmedi
            };

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Oyun sepete eklenmiştir.";
            return RedirectToAction(nameof(Checkout));
        }
        // Sepetten oyun silme
        public async Task<IActionResult> RemoveFromCart(int purchaseId)
        {
            var userId = User.Identity?.Name;

            // Kullanıcıya ait sepet kaydını alıyoruz
            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(p => p.Id == purchaseId && p.UserId == userId);

            if (purchase == null)
            {
                TempData["ErrorMessage"] = "Sepette böyle bir oyun bulunamadı.";
                return RedirectToAction(nameof(Checkout));
            }

            // Oyun sepetten siliniyor
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Oyun sepetinizden başarıyla silindi.";
            return RedirectToAction(nameof(Checkout));
        }

    }
}
