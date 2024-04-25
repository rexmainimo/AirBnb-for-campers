using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public class CampingSpotData : ICampingSpot
    {
        readonly Database db = new Database();

        public bool Addcampingspot(CampingSpot spot)
        {
            // Define the SQL query with placeholders for parameters
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

            // Execute the query with parameters using the ExecuteQuery method
            return db.ExecuteQuery(query, parameters);
        }

        /*public void Addcampingspot(CampingSpot spot)
        {
            string query = $"INSERT INTO `campingspots`(`CampingSpot_id`, `Name`, `Location`, `Description`, `Facilities`, `Availability`, `Owner_id`) " +
                     $"VALUES ('{spot.Id}', '{spot.Name}', '{spot.Location}', '{spot.Description}', '{spot.Facilities}', '{spot.Availability}', '{spot.Owner_Id}');";

            db.ExecuteQuery(query);
        }*/
        /*public IEnumerable<CampingSpot> GetCampingSpots()
        {
            string query = $"SELECT * FROM `campingspots`;";
            if(db.ExecuteQuery(query))
            {
                return
            }
        }*/


    }
}
