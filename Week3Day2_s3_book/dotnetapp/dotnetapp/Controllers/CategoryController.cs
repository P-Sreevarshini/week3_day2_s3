using dotnetapp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using dotnetapp.Models;

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
