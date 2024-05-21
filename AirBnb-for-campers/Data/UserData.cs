using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb_for_campers.Data
{
    public class UserData : IUsers
    {
        private readonly Database db = new Database();

        public bool CreateNewUser(User newUser)
        {
            string query = "INSERT INTO `Users` (`FirstName`, `LastName`, `UserName`, `Email`, `PASSWORD`, `PhoneNum`, `LogDateTime`) " + 
                "VALUES (@FirstName, @LastName, @UserName, @Email, @PASSWORD, @PhoneNum, @LogDateTime)";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@FirstName", newUser.FirstName },
                {"@LastName", newUser.LastName },
                {"@UserName", newUser.UserName },
                {"@Email", newUser.Email },
                {"@PASSWORD", newUser.Password },
                {"@PhoneNum", newUser.PhoneNum },
                {"@LogDateTime", newUser.LogDateTime }
                /*{"@ProfilePictureUrl", newUser.ProfilePictureUrl }*/
            };

            return db.ExecuteQuery(query, parameters);

        }
        public bool Logging(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Password) ||
                string.IsNullOrEmpty(loginRequest.Username))
            {
                return false;
            }
            else
            {// Use this line to retrieve user_id for userhome processes in the frontend
                string query = $"SELECT COUNT(*) FROM `Users` WHERE `FirstName` = @FirstName AND PASSWORD = @password;";

                Dictionary<string, Object> paramters = new Dictionary<string, object>
                {
                    { "@FirstName", loginRequest.Username },
                    { "@PASSWORD", loginRequest.Password },
                };
                int count = db.VerifyLoggingInfor<int>(query, paramters);

                return count > 0;
                
            }

            
        }

        public bool UploadProfilePictureToDb(int userId, string profilePictureUrl)
        {
            string query = "UPDATE Users SET ProfilePictureUrl = @profilePictureUrl WHERE User_id = @userId";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@profilePictureUrl", profilePictureUrl },
                {"@userId", userId }
            };

            if(db.ExecuteQuery(query, parameters) == true)
            {
                return true;            }
            else
            {
                return false;
            }
        }

        public string GetProfilePictureUrl(int userId)
        {
            string query = "SELECT ProfilePictureUrl FROM Users WHERE User_id = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@userId", userId}
            };
            return db.ExecuteScalar(query, parameters)?.ToString();
        }
        public bool DeleteProfilePicture(int userId)
        {
            string query = "UPDATE Users SET ProfilePictureUrl = NULL WHERE User_id = @userId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@userId", userId }
            };
            return db.ExecuteQuery(query, parameters);
        }



    }
}
