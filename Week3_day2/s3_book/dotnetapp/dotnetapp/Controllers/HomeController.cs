// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data; // Add the correct namespace for ApplicationDbContext


public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View(books);
    }
}
