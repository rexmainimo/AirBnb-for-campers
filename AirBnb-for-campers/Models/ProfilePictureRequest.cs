using System.ComponentModel.DataAnnotations;

namespace AirBnb_for_campers.Models
{
    public class ProfilePictureRequest
    {
        [Required]
        public string ProfilePictureUrl { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
