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
        bool UploadProfilePictureToDb(int userId, string userProfileUrl);

        bool UpdateUserInfo(User user);
    }
}
