using System.ComponentModel.DataAnnotations.Schema;

namespace AirBnb_for_campers.Models
{
    public class BookingInfo
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfPeople { get; set; }
        public decimal Price { get; set; }
        public string CampingSpotName { get; set; }
        public string CampingSpotCity { get; set; }
        public string PostalNum { get; set; }
        public int StreetNum {  get; set; }
        public int HouseNum {  get; set; }
        public string CampingSpotDescription { get; set; }
        public string CampingSpotFacilities { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public byte[]? ImageData { get; set; } // Property to hold the image data
        [NotMapped]
        public string? ImageContentType { get; set; } // Propert

    }
}
