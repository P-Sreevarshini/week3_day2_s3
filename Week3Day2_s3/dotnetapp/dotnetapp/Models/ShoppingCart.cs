namespace dotnetapp.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public List<ShoppingCartItem> Items { get; set; }
    }
}
