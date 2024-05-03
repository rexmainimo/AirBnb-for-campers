using AirBnb_for_campers.Models;
using MySqlConnector;

namespace AirBnb_for_campers.Data
{
    public class CampingSpotData : ICampingSpot
    {
        private readonly Database db = new Database();

        public bool Addcampingspot(CampingSpot spot)
        {
            // SQL query with placeholders for parameters
            string query = "INSERT INTO `campingspots` (`Name`, `Location`, `Description`, `Facilities`, `Availability`, `Owner_id`) " +
                           "VALUES (@Name, @Location, @Description, @Facilities, @Availability, @Owner_Id)";

            // Create a dictionary to hold the parameter values
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
                { "@Name", spot.Name },
                { "@Location", spot.Location },
                { "@Description", spot.Description },
                { "@Facilities", spot.Facilities },
                { "@Availability", spot.Availability },
                { "@Owner_Id", spot.Owner_Id }
            };

            // Execute the query with parameters using the ExecuteQuery method in the database class
            return db.ExecuteQuery(query, parameters);
        }
        public IEnumerable<CampingSpot> GetCampingSpots()
        {
            string query = "SELECT * FROM `campingspots`;";

            return db.ExtractQuery(query); 
        }
        public IEnumerable<CampingSpot> GetSpotByName(string name)
        {
            string query = $"SELECT CampingSpot_id, Name, Location, Description, Facilities, Availability, Owner_id FROM `campingspots` WHERE Name LIKE '%{name}%';";
            return db.ExtractQueryByName(query);
        }
    }
}
