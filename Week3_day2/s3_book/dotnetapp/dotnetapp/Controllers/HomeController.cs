using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data;
using dotnetapp.ViewModels;
using System.Linq;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            FeaturedBooks = _context.Books.ToList()
        };
        return View(model);
    }
}
