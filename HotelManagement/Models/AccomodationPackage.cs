namespace HotelManagement.Models
{
    public class AccomodationPackage
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NumberOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
    }
}
