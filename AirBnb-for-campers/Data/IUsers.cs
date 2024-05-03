using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IUsers
    {
        bool CreateNewUser(User newUser);

        bool Logging(string username, string password);
    }
}
