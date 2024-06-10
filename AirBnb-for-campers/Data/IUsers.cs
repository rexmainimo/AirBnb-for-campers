using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IUsers
    {
        bool CreateNewUser(User newUser);
        int? Logging(LoginRequest loginRequest);
        IEnumerable<User> GetUserInfo(int user_id);
        string GetUserName(int userId);
        string GetUserPictureUrl(int id);
        string UploadProfilePictureToDb(string userProfileUrl, int userId);
        string GetProfilePictureUrl(int userId);
        bool DeleteProfilePicture(int userId);

        bool UpdateUserInfo(User user);
    }
}
