using System.Buffers.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirBnb_for_campers.Models
{
    public class CampingSpot
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PostalNum { get; set; }
        public int StreetNum {  get; set; }
        public int? HouseNum { get; set; }
        public string Description { get; set; }
        public string Facilities { get; set; }
        public string Availability { get; set; }
        public IFormFile ImageFile {  get; set; }
        public string? ImageUrl { get; set; }
        public int Owner_Id { get; set; }

        [NotMapped]
        public byte[] ImageData { get; set; } // Property to hold the image data
        [NotMapped]
        public string ImageContentType { get; set; } // Property to hold the image content type
    }
    /* public double Latitude { get; set; }// 40.712776;
     public double Longitude { get; set; }*/ //-74.005974;


}

