using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Repository;

namespace dotnetapp.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public Order GetOrder(int id)
        {
            return _orderRepository.GetOrder(id);
        }

        public Order SaveOrder(Order order)
        {
            return _orderRepository.SaveOrder(order);
        }

        public Order UpdateOrder(int id, Order order)
        {
            return _orderRepository.UpdateOrder(id, order);
        }

        public bool DeleteOrder(int id)
        {
            return _orderRepository.DeleteOrder(id);
        }
    }
}
