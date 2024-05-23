using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IUsers
    {
        bool CreateNewUser(User newUser);
        int? Logging(LoginRequest loginRequest);
        IEnumerable<User> GetUserInfo(int user_id);
        bool UploadProfilePictureToDb(int userId, string userProfileUrl);
        string GetProfilePictureUrl(int userId);
        bool DeleteProfilePicture(int userId);

        bool UpdateUserInfo(User user);
    }
}
