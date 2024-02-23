namespace dotnetapp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public List<Order>? Orders { get; set; }
        //public List<Booking> Bookings { get; set; }


    }
}
