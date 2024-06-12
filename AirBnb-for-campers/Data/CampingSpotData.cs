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
               
                string query = "INSERT INTO `campingspots` (`Name`, `City`, `PostalNum`, `StreetNum`, `HouseNum`, `Description`, `Facilities`, `Availability`, `Image`, `Owner_id`) " +
                               "VALUES (@Name, @City, @PostalNum, @StreetNum, @HouseNum, @Description, @Facilities, @Availability, @Image, @Owner_Id)";

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
                        {"@Image", spot.ImageUrl },
                        { "@Owner_Id", spot.Owner_Id },
                       /* {"@Latitude", spot.Latitude},
                        {"@Longitude", spot.Longitude }*/
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

        public IEnumerable<CampingSpot> GetOwnerCampingSpot(int id)
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
