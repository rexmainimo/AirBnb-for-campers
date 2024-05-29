namespace AirBnb_for_campers.Models
{
    public class BookingInfo
    {
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfPeople { get; set; }
        public decimal Price { get; set; }
        public string CampingSpotName { get; set; }
        public string CampingSpotLocation { get; set; }
        public string CampingSpotDescription { get; set; }
        public string CampingSpotFacilities { get; set; }

    }
}
