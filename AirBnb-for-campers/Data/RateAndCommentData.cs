using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public class RateAndCommentData : IRatingsAndComments
    {
        private readonly Database db = new Database();

        public bool IsRatingOrComment(RatingsAndComments ratingsAndComments)
        {
            string query = "INSERT INTO `RatingsAndComments` (`Rating`, `Comment`, `User_id`, `CampingSpot_id`) " +
                "VALUES (@Rating, @Comment, @User_id, @CampingSpot_id)";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Rating", ratingsAndComments.Rating},
                {"@Comment", ratingsAndComments.Comment },
                {"@User_id", ratingsAndComments.User_id },
                {"@CampingSpot_id", ratingsAndComments.CampingSpot_id }
            };
            return db.ExecuteQuery(query, parameters);
        }
        public IEnumerable<RateAndCommentInfo> GetRatingsAndComments()
        {
            // fix the query, not retrieving what is wanted.
            string query = "SELECT RatingsAndComments.Rating, RatingsAndComments.Comment, Users.FirstName, CampingSpots.Name " +
                "FROM RatingsAndComments " +
                "INNER JOIN Users ON RatingsAndComments.User_id = Users.User_id " +
                "INNER JOIN CampingSpots ON RatingsAndComments.CampingSpot_id = CampingSpots.CampingSpot_id";
                //"WHERE RatingsAndComments.User_id = @User_id";
           /* Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@User_id", id },
            };*/
            return db.GetRatingsAndComments(query);
        }

    }
}
