using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data;
using dotnetapp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{

    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var shoppingCart = _context.ShoppingCarts
            .Include(cart => cart.Items)
            .ThenInclude(item => item.Book);
            //.FirstOrDefault(cart => cart.SessionId == sessionId);

            var model = new ShoppingCartViewModel
            {
                //Cart = shoppingCart
            };
            return View(model);
        }

        // Add actions for adding, updating, and removing items from the cart
    }
}