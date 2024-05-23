using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public class OwnerDatabase : IOwners
    {
        private readonly Database db = new Database();
        public bool CreateNewOwner(Owner newOwner)
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
        public int? OwnerLogin(OwnerLoginRequest owner)
        {
            if(owner == null || string.IsNullOrEmpty(owner.Password) || owner.Id == null)
            {
                return null;
            }
            else
            {
                string query = "SELECT `Owner_id` FROM `Owners` WHERE Owner_id = @ownerId AND PASSWORD = @Password";
                Dictionary<string, object> paramters = new Dictionary<string, object>
                {
                    {"@ownerId", owner.Id },
                    {"@Password", owner.Password }
                };
                return db.VerifyOwnerLogin(query, paramters);
                
            }
        }
    }
}
