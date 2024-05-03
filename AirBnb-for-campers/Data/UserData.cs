using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public class UserData : IUsers
    {
        private readonly Database db = new Database();

        public bool CreateNewUser(User newUser)
        {
            string query = "INSERT INTO `Users` (`FirstName`, `LastName`, `Email`, `PASSWORD`, `PhoneNum`, `LogDateTime`) " + 
                "VALUES (@FirstName, @LastName, @Email, @PASSWORD, @PhoneNum, @LogDateTime)";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@FirstName", newUser.FirstName },
                {"@LastName", newUser.LastName },
                {"@Email", newUser.Email },
                {"@PASSWORD", newUser.Password },
                {"@PhoneNum", newUser.PhoneNum },
                {"@LogDateTime", newUser.LogDateTime },
            };

            return db.ExecuteQuery(query, parameters);

        }
        public bool Logging(string username, string password)
        {
            if (username == null || password == null)
            {
                return false;
            }
            else
            {
                string query = $"SELECT COUNT(*) FROM `Users` WHERE `FirstName` = @FirstName AND PASSWORD = @password;";

                Dictionary<string, Object> paramters = new Dictionary<string, object>
                {
                    { "@FirstName", username },
                    { "@PASSWORD", password },
                };
                int count = db.VerifyLoggingInfor<int>(query, paramters);

                return count > 0;
                
            }

            
        }

       
    }
}
