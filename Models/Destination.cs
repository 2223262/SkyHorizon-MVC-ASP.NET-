namespace SkyHorizon_2223262.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int DistanceFromPortugal { get; set; }
    }
}
