using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class OrderRepository
    {
        private static List<Order> _orders = new List<Order>()
        {
            new Order { OrderId = 1, CustomerName = "Customer 1", TotalAmount = 100.50m },
            new Order { OrderId = 2, CustomerName = "Customer 2", TotalAmount = 200.75m },
            new Order { OrderId = 3, CustomerName = "Customer 3", TotalAmount = 150.25m }
        };

        public List<Order> GetOrders() => _orders;

        public Order GetOrder(int id) => _orders.FirstOrDefault(o => o.OrderId == id);

        public void SaveOrder(Order order)
        {
            _orders.Add(order);
        }

        public void UpdateOrder(int id, Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == id);
            if (existingOrder != null)
            {
                existingOrder.CustomerName = order.CustomerName;
                existingOrder.TotalAmount = order.TotalAmount;
            }
        }

        public void DeleteOrder(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == id);
            if (orderToRemove != null)
            {
                _orders.Remove(orderToRemove);
            }
        }
    }
}
