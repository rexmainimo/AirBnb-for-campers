namespace AirBnb_for_campers.Models
{
    public class CampingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Facilities { get; set; }

        public string Availability { get; set; }
        public int Owner_Id { get; set; }

       
    }
}
