using dotnetapp.Models;

namespace dotnetapp.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public List<Book> Books { get; set; }
    }

}
