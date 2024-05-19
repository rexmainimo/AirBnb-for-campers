using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IUsers
    {
        bool CreateNewUser(User newUser);

        bool Logging(LoginRequest loginRequest);
        bool UploadProfilePicture(int userId, string userProfileUrl);
    }
}
