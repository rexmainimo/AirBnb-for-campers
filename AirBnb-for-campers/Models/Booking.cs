namespace AirBnb_for_campers.Models
{
    public class Booking
    {
        public int Id { get; set; }
        User User { get; set; }
        CampingSpot CampingSpot { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
        public decimal TotalPrice { get; set; }


    }
}
