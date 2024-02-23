using System.Data;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/orders
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            //var orders = await _dbContext.Orders.Include(o => o.OrderItems).ToListAsync();
            var orders = await _dbContext.Orders
    .Include(o => o.OrderItems)
    .ThenInclude(oi => oi.Product)  // Include the Product associated with each OrderItem
    .Include(o => o.Customer)          // Include the Products associated with each Order
    .ToListAsync();
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("customer/{custometid}")]
        public async Task<ActionResult<Order>> GetOrderByCustomerId(int custometid)
        {
            //var order = await _dbContext.Orders.Include(o => o.OrderItems)
            //                                   .FirstAsync(o => o.CustomerID == custometid);
            var orders = await _dbContext.Orders
    .Include(o => o.OrderItems)
    .Where(o => o.CustomerID == custometid)
    .ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [Authorize]
        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderItems)

    .ThenInclude(oi => oi.Product)  // Include the Product associated with each OrderItem
    .Include(o => o.Customer).FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [Authorize(Roles = "InventoryManager")]
        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> PlaceOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderID }, order);
        }

        [Authorize]
        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order updatedOrder)
        {
            if (id != updatedOrder.OrderID)
            {
                return BadRequest();
            }

            _dbContext.Entry(updatedOrder).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "InventoryManager")]
        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            //var order = await _dbContext.Orders.FindAsync(id);
            var order = await _dbContext.Orders.Include(o => o.OrderItems)

    .ThenInclude(oi => oi.Product)  // Include the Product associated with each OrderItem
    .Include(o => o.Customer).FirstOrDefaultAsync(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok(order);
                }


        [Authorize(Roles = "InventoryManager")]
        [HttpPost("{orderId}/items")]
        public async Task<ActionResult<OrderItem>> AddOrderItem(int orderId, OrderItem orderItem)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderItems)
                                               .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }
            orderItem.OrderID = orderId;
            Console.WriteLine(orderItem.ProductID);
            Console.WriteLine(orderItem.Quantity);
            var product = await _dbContext.Products.FindAsync(orderItem.ProductID);

            if (product == null)
            {
                return NotFound($"Product with ID {orderItem.ProductID} not found.");
            }

            decimal totalAmount = orderItem.Quantity * product.Price;
Console.WriteLine(totalAmount);
            Console.WriteLine(order.TotalAmount);
            order.TotalAmount += totalAmount;
            Console.WriteLine(order.TotalAmount);


            // Add validation or other logic as needed

            order.OrderItems.Add(orderItem);
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Status = "Success", Message = "Product added to order successfully" });
        }


        private bool OrderExists(int id)
        {
            return _dbContext.Orders.Any(o => o.OrderID == id);
        }
    }
}
