using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IUsers
    {
        bool CreateNewUser(User newUser);

        bool Logging(LoginRequest loginRequest);
        bool UploadProfilePictureToDb(int userId, string userProfileUrl);
        string GetProfilePictureUrl(int userId);
        bool DeleteProfilePicture(int userId);
    }
}
