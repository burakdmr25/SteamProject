namespace BurakSteam.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Genre { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public string? VideoPath { get; set; }
        public virtual GameDetails GameDetails { get; set; } = new GameDetails();



    }

}
