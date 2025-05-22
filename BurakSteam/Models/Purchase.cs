using BurakSteam.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BurakSteam.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string? UserId { get; set; } // ApplicationUser'dan gelen UserId, Identity UserId
        public int GameId { get; set; }
        public DateTime? PurchaseDate { get; set; } = DateTime.Now;
        public int Price { get; set; }

        // Navigasyon Özelliklerini virtual olarak işaretle
        public virtual ApplicationUser? User { get; set; } // Kullanıcıyı referans almak için ApplicationUser
        public virtual Game? Game { get; set; } // Oyunla ilişkili
    }
}
