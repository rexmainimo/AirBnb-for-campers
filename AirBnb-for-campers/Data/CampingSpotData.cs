using AirBnb_for_campers.Models;
using MySqlConnector;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AirBnb_for_campers.Data
{
    public class CampingSpotData : ICampingSpot
    {
        private readonly Database db = new Database();

        public bool Addcampingspot(CampingSpot spot)
        {
            try
            {
                // convert base64 string to image
                /* byte[] imageBytes = Convert.FromBase64String(spot.Image);
                 using (MemoryStream ms = new MemoryStream(imageBytes))
                 {
                     Image image = Image.FromStream(ms);

                     string imageName = Guid.NewGuid().ToString() + ".jpg";

                     string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);
                     image.Save(imagePath, ImageFormat.Jpeg);


                 }*/
                // SQL query with placeholders for parameters
                string query = "INSERT INTO `campingspots` (`Name`, `City`, `PostalNum`, `StreetNum`, `HouseNum`, `Description`, `Facilities`, `Availability`, `Image`, `Owner_id`, `Latitude`, `Longitude`) " +
                               "VALUES (@Name, @City, @PostalNum, @StreetNum, @HouseNum, @Description, @Facilities, @Availability, @Image, @Owner_Id, @Latitude, @Longitude)";

                // Create a dictionary to hold the parameter values
                Dictionary<string, object> parameters = new Dictionary<string, object>
    {
                        { "@Name", spot.Name },
                        { "@City", spot.City },
                        {"@PostalNum", spot.PostalNum },
                        {"@StreetNum", spot.StreetNum },
                        {"@HouseNum",   spot.HouseNum },
                        { "@Description", spot.Description },
                        { "@Facilities", spot.Facilities },
                        { "@Availability", spot.Availability },
                        {"@Image", spot.Image },
                        { "@Owner_Id", spot.Owner_Id },
                        {"@Latitude", spot.Latitude},
                        {"@Longitude", spot.Longitude }
                    };

                // Execute the query with parameters using the ExecuteQuery method in the database class
                return db.ExecuteQuery(query, parameters);


            }
            catch (Exception ex)
            {
                throw new Exception("Error adding camping spot: " + ex.Message);
            }
        }
        public IEnumerable<CampingSpot> GetCampingSpots()
        {
            string query = "SELECT * FROM `campingspots`;";

            return db.ExtractQuery(query); 
        }
        public IEnumerable<CampingSpot> GetSpotByName(string name)
        {
            string query = $"SELECT CampingSpot_id, Name, City, PostalNum, StreetNum, HouseNum, Description, Facilities, Availability, Image, Owner_id FROM `campingspots` WHERE Name LIKE '%{name}%';";
            return db.ExtractCampingFilter(query);
        }
        public IEnumerable<CampingSpot> GetSpotByCity(string city)
        {
            string query = $"SELECT CampingSpot_id, Name, City, PostalNum, StreetNum, HouseNum, Description, Facilities, Availability, Image, Owner_id FROM `campingspots` WHERE City LIKE '%{city}%';";
            return db.ExtractCampingFilter(query);
        }
        public IEnumerable<CampingSpot> GetSpotByDescription(string description)
        {
            string query = $"SELECT CampingSpot_id, Name, City, PostalNum, StreetNum, HouseNum, Description, Facilities, Availability, Image, Owner_id FROM `campingspots` WHERE Description LIKE '%{description}%';";
            return db.ExtractCampingFilter(query);
        }
        public IEnumerable<CampingSpot> GetSpotByAvailability(string availability)
        {
            string query = $"SELECT CampingSpot_id, Name, City, PostalNum, StreetNum, HouseNum, Description, Facilities, Availability, Image, Owner_id FROM `campingspots` WHERE Availability = '{availability}';";
            return db.ExtractCampingFilter(query);
        }

        public IEnumerable<CampingSpot> GetOwnerCampingspot(int id)
        {
            string query = "SELECT * FROM `campingspots` WHERE Owner_id = @id";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            return db.ExtractOwnerSpots(query, parameters);
        }
    }
}
