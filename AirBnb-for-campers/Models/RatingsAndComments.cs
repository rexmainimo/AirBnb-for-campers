namespace AirBnb_for_campers.Models
{
    public class RatingsAndComments
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int User_id {  get; set; }   
        public int CampingSpot_id { get; set; }
        //public DateTime CreatedAt { get; set; }

    }
}
