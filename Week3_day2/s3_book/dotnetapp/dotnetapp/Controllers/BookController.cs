// BookController.cs
using Microsoft.AspNetCore.Mvc;

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

// CategoryController.cs
using Microsoft.AspNetCore.Mvc;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int categoryId)
    {
        var category = _context.Categories.Include(c => c.Books).FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
}