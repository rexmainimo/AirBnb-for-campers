using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public class OwnerDatabase : IOwners
    {
        private readonly Database db = new Database();
        public bool CreateNewOwner(Owner newOwner)
        {
            if (checkOwner(newOwner.Email))
            {
                string query = "INSERT INTO `Owners` (`FirstName`, `LastName`, `Email`, `PASSWORD`, `PhoneNum`) " +
                "VALUES (@FirstName, @LastName, @Email, @Password, @PhoneNum)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                {"@FirstName", newOwner.FirstName},
                {"@LastName", newOwner.LastName },
                {"@Email", newOwner.Email },
                {"@Password", newOwner.Password },
                {"@PhoneNum", newOwner.PhoneNum }
                };
                return db.ExecuteQuery(query, parameters);
            }
            return false;
        }
        private bool checkOwner(string email)
        {
            string query = "SELECT COUNT(*) FROM `Owners` WHERE `Email` = @Email";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Email", email},
            };
            int count = db.ExecuteScalar(query, parameters);
            return count == 0;

        }
        public int? OwnerLogin(LoginRequest owner)
        {
            if(owner == null || string.IsNullOrEmpty(owner.Password) || string.IsNullOrEmpty(owner.Email))
            {
                return null;
            }
            else
            {
                string query = "SELECT `Owner_id` FROM `Owners` WHERE Email = @Email AND PASSWORD = @Password";
                Dictionary<string, object> paramters = new Dictionary<string, object>
                {
                    {"@Email", owner.Email },
                    {"@Password", owner.Password }
                };
                return db.VerifyOwnerLogin(query, paramters);
                
            }
        }
    }
}
