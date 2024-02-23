using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
        [JsonIgnore]
        public List<Product>? Products { get; set; }
        //public List<Booking>? Bookings { get; set; }
    }
}
