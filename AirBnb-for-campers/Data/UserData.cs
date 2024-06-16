using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace AirBnb_for_campers.Data
{
    public class UserData : IUsers
    {
        private readonly Database db = new Database();

        public bool CreateNewUser(User newUser)
        {
            if (checkUser(newUser.Email))
            {
                string query = "INSERT INTO `Users` (`FirstName`, `LastName`, `Email`, `PASSWORD`, `PhoneNum`, `LogDateTime`) " +
               "VALUES (@FirstName, @LastName, @Email, @PASSWORD, @PhoneNum, @LogDateTime)";

                Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@FirstName", newUser.FirstName },
                {"@LastName", newUser.LastName },
                {"@Email", newUser.Email },
                {"@PASSWORD", newUser.Password },
                {"@PhoneNum", newUser.PhoneNum },
                {"@LogDateTime", newUser.LogDateTime }
                /*{"@ProfilePictureUrl", newUser.ProfilePictureUrl }*/
                };

                return db.ExecuteQuery(query, parameters);
            }
            return false;
           

        }
        private bool checkUser(string email)
        {
            string query = "SELECT COUNT(*) FROM `Users` WHERE `Email` = @Email";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Email", email},
            };
            int count = db.ExecuteScalar(query, parameters);
            return count == 0;

        }
        public int? Logging(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Password) ||
                string.IsNullOrEmpty(loginRequest.Email))
            {
                return null;
            }

            // Use this line to retrieve user_id for userhome processes in the frontend
            string query = $"SELECT `User_id` FROM `Users` WHERE `Email` = @Email AND PASSWORD = @password;";

            Dictionary<string, Object> paramters = new Dictionary<string, object>
            {
                { "@Email", loginRequest.Email },
                { "@PASSWORD", loginRequest.Password },
            };
            return db.VerifyUserLoggin(query, paramters);

        }
        public IEnumerable<User> GetUserInfo(int userId)
        {
            
            string query = "SELECT `FirstName`, `LastName`, `Email`, `PhoneNum`, `PASSWORD` " +
                "FROM `Users` WHERE `User_id` = @userId";
            Dictionary<string, object> parameter = new Dictionary<string, object>
            {
                { "@userId", userId }
            };
            return db.ExtractUserInfo(query, parameter);
        }
        public bool UpdateUserInfo(User user)
        {
            //method update user personal information
            if (user == null || user.Id == 0)
            {
                throw new ArgumentException("User or user ID cannot be null or 0");
            }

            var updates = new List<string>();
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(user.FirstName))
            {
                updates.Add("`FirstName` = @FirstName");
                parameters["@FirstName"] = user.FirstName;
            }
            if (!string.IsNullOrEmpty(user.LastName))
            {
                updates.Add("`LastName` = @LastName");
                parameters["@LastName"] = user.LastName;
            }
          
            if (!string.IsNullOrEmpty(user.Email))
            {
                updates.Add("`Email` = @Email");
                parameters["@Email"] = user.Email;
            }
            if (user.PhoneNum != 0)
            {
                updates.Add("`PhoneNum` = @PhoneNum");
                parameters["@PhoneNum"] = user.PhoneNum;
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                updates.Add("`Password` = @Password");
                parameters["@Password"] = user.Password;
            }

            if (updates.Count == 0)
            {
                return false; // No fields to update
            }

            string query = $"UPDATE `Users` SET {string.Join(", ", updates)} WHERE `User_id` = @UserId";
            parameters["@UserId"] = user.Id;

            return db.ExecuteQuery(query, parameters);
        }
        public string GetUserName(int id)
        {
            string query = "SELECT `FirstName`, `LastName` FROM `Users` WHERE `User_id` = @id";
            Dictionary<string, object> paramters = new Dictionary<string, object>
            {
                {"@id", id }
            };
            return db.ExtractUser(query, paramters);
        }
        public string GetUserPictureUrl(int id)
        {
            string query = "SELECT `profilePictureUrl` FROM `Users` WHERE `User_id` = @id";
            Dictionary<string, object> paramters = new Dictionary<string, object>
            {
                {"@id", id }
            };
            return db.ExtractUserPictureUrl(query, paramters);
        }

        public bool UploadProfilePictureToDb(int userId, string profilePictureUrl)
        {
            string query = "UPDATE Users SET ProfilePictureUrl = @profilePictureUrl WHERE User_id = @userId";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@profilePictureUrl", profilePictureUrl },
                {"@userId", userId }
            };

            bool isQuerySuccessful = db.ExecuteQuery(query, parameters);
           
            return isQuerySuccessful;
        }

    }
}
