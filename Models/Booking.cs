namespace SkyHorizon_2223262.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public string OriginCountry { get; set; } = null!;
        public string SeatType { get; set; } = null!;
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Passengers { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual Destination? Destination { get; set; }
    }
}
