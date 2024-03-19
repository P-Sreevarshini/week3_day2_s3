// BookController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data; // Add the correct namespace for ApplicationDbContext

public class BookController : Controller
{
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
        return View(book);
    }

    [HttpPost]
    public IActionResult AddToCart(int bookId, int quantity)
    {
        // Add logic to add book to shopping cart
        return RedirectToAction("Index", "ShoppingCart");
    }
}

