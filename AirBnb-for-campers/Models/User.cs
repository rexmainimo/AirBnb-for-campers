namespace AirBnb_for_campers.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }   
        public string  UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNum { get; set; }  
        public DateTime LogDateTime { get; private set; } = DateTime.Now;
        public string ProfilePictureUrl {  get; set; }
    }
}
