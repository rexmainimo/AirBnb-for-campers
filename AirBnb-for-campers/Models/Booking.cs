namespace AirBnb_for_campers.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; } //= DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfPeople { get; set; }
        public decimal Price { get; set; }
        public int User_id { get; set; }
        public int CampinpingSpot_id { get; set; }
        


    }
}
