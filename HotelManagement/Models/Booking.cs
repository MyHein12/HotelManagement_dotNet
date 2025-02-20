namespace HotelManagement.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public int AccomodationID { get; set; }
        public Accomodation Accomodation { get; set; }
        public DateTime FromDate { get; set; }
        
        //number of stay nights
        public int Duration { get; set; }
    }
}
