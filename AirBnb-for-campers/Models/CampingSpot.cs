namespace AirBnb_for_campers.Models
{
    public class CampingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Facilities { get; set; }

        public bool Availability { get; set; }

        Owner Owner { get; set; }
    }
}
