using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> _orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerName = "Customer 1", TotalAmount = 100.50m },
            new Order { OrderId = 2, CustomerName = "Customer 2", TotalAmount = 200.75m }
        };

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }

        public Order GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.OrderId == id);
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
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
