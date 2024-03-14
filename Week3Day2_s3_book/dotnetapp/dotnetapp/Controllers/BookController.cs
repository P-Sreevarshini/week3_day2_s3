using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace dotnetapp.Controllers
{
    public class BookController : Controller
    {
        private static ShoppingCart shoppingCart = new ShoppingCart();

        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }
            Console.WriteLine(book.Title);
            return View(book);
        }



        [HttpPost]
        public IActionResult AddToCart(int bookId, int quantity)
        {
            Console.WriteLine(bookId);
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            // Check if the book is already in the cart, and update the quantity if it is
            var existingItem = shoppingCart.Items.FirstOrDefault(item => item.BookId == bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                shoppingCart.Items.Add(new ShoppingCartItem
                {
                    BookId = bookId,
                    Quantity = quantity
                });
            }

            return RedirectToAction("Cart");
        }


        // Implement methods for getting/creating the shopping cart and other actions as needed
    }
}