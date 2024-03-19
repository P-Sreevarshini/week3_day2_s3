using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add the correct namespace for Include method
using System.Linq; // Add the correct namespace for IQueryable extension methods
using dotnetapp.Data; // Add the correct namespace for ApplicationDbContext

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
