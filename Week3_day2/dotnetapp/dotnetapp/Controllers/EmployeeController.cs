using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Data;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Create(Employee employee)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         // Validation passed, add the employee to the database
    //         _context.Employees.Add(employee);
    //         _context.SaveChanges();
    //         return RedirectToAction("Success");
    //     }

    //     // Validation failed, return to the Create view with error messages
    //     return View(employee);
    // }
    // POST: Employee/Create
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Employee employee)
{
    if (ModelState.IsValid)
    {
        // Validation passed, add the employee to the database
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return RedirectToAction("Success");
    }

    // Validation failed, return to the Create view with error messages
    return View(employee);
}


    public IActionResult Success()
    {
       return View();
    }

    // ... (other controller actions for Edit, Delete, Index, etc.)
}
