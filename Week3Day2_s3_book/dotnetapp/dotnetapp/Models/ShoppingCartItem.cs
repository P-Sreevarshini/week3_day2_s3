using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
