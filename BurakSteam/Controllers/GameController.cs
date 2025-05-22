using BurakSteam.Data;
using BurakSteam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BurakSteam.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor - Veritabanı bağlamını alır
        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Oyun listesini sayfalama ile gösterir
        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;

            var totalGames = _context.Games.Count();
            var games = _context.Games
                                .Include(g => g.GameDetails)  // GameDetails'i dahil et
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalGames / pageSize);
            ViewBag.CurrentPage = page;

            return View(games);
        }

        // Yeni oyun ekleme sayfasını gösterir
        public IActionResult Create()
        {
            return View();
        }

        // Yeni oyun ekleme işlemini yapar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Game game, IFormFile? imageFile, IFormFile? videoFile)
        {
            if (ModelState.IsValid)
            {
                // Resim ve video dosyalarını kaydetme
                if (imageFile != null)
                {
                    var imageFileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var imagePath = Path.Combine("wwwroot/images", imageFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    game.ImagePath = $"/images/{imageFileName}";
                }

                if (videoFile != null)
                {
                    var videoFileName = Path.GetFileNameWithoutExtension(videoFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(videoFile.FileName);
                    var videoPath = Path.Combine("wwwroot/videos", videoFileName);
                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                    }
                    game.GameDetails.VideoPath = $"/videos/{videoFileName}"; // VideoPath atanıyor
                }

                // GameDetails kontrolü
                if (game.GameDetails == null)
                {
                    game.GameDetails = new GameDetails();
                }

                // Genre'yi manuel olarak ekleyin
                game.GameDetails.Genre = game.Genre;

                _context.Games.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(game);
        }

        // Oyun detaylarını gösterir
        public IActionResult Details(int id)
        {
            var game = _context.Games
         .Include(g => g.GameDetails) // GameDetails'i dahil et
         .FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game); // game nesnesini View'a gönder
        }

        // GameDetails için ayrı detay sayfasını oluşturur
        public IActionResult GameDetails(int id)
        {
            var game = _context.Games
                .Include(g => g.GameDetails)  // GameDetails'i dahil et
                .FirstOrDefault(g => g.Id == id);

            if (game == null || game.GameDetails == null)
            {
                return NotFound();
            }

            return View(game); // Game objesini gönder
        }

        // Oyun düzenleme işlemleri
        public IActionResult Edit(int id)
        {
            var game = _context.Games
                                .Include(g => g.GameDetails)  // GameDetails'i dahil et
                                .FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Game updatedGame, IFormFile? newImageFile, IFormFile? newVideoFile)
        {
            if (id != updatedGame.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var game = _context.Games.Include(g => g.GameDetails).FirstOrDefault(g => g.Id == id);

                if (game == null)
                {
                    return NotFound();
                }

                // Oyun bilgilerini güncelleme
                game.Name = updatedGame.Name;
                game.Genre = updatedGame.Genre;
                game.Price = updatedGame.Price;
                game.Description = updatedGame.Description;

                // Resim güncelleme
                if (newImageFile != null)
                {
                    var imageFileName = Path.GetFileNameWithoutExtension(newImageFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(newImageFile.FileName);
                    var imagePath = Path.Combine("wwwroot/images", imageFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await newImageFile.CopyToAsync(stream);
                    }
                    game.ImagePath = $"/images/{imageFileName}";
                }

                // Video güncelleme
                if (newVideoFile != null)
                {
                    var videoFileName = Path.GetFileNameWithoutExtension(newVideoFile.FileName) + "_" + Guid.NewGuid() + Path.GetExtension(newVideoFile.FileName);
                    var videoPath = Path.Combine("wwwroot/videos", videoFileName);
                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await newVideoFile.CopyToAsync(stream);
                    }
                    game.VideoPath = $"/videos/{videoFileName}";
                }

                // GameDetails bilgilerini güncelleme
                if (game.GameDetails == null)
                {
                    game.GameDetails = new GameDetails();
                }

                _context.Update(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(updatedGame);
        }

        // Oyun silme işlemleri
        public IActionResult Delete(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
