// Book.cs
using System.ComponentModel.DataAnnotations;
namespace dotnetapp.Models
{
public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    public string Description { get; set; }

    public string CoverImage { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
}
}