// ShoppingCartItem.cs
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
public class ShoppingCartItem
{
    public int Id { get; set; }

    public int BookId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    public Book Book { get; set; }
}
}