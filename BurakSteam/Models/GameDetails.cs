namespace BurakSteam.Models
{
    public class GameDetails
    {
        public int Id { get; set; }
        public int GameId { get; set; } // İlişkili oyun
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
        public string? Publisher { get; set; }
        public string? Series { get; set; }
        public string? Languages { get; set; } // Dillerin bir listesini tutabilirsiniz

        public string? VideoPath { get; set; } // Video yolu için özellik

        // İlişkili Oyun
        public virtual Game? Game { get; set; }
    }
}
