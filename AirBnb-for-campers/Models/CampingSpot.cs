using System.Buffers.Text;

namespace AirBnb_for_campers.Models
{
    public class CampingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PostalNum { get; set; }
        public int StreetNum {  get; set; }
        public int? HouseNum { get; set; }
        public string Description { get; set; }
        public string Facilities { get; set; }
        public string Availability { get; set; }
        public string Image {  get; set; }
        public int Owner_Id { get; set; }
        public double Latitude { get; set; }// 40.712776;
        public double Longitude { get; set; }// -74.005974;
        //public string? ImageBase64 { get; set; }  // New property for base64-encoded image

    }
}
