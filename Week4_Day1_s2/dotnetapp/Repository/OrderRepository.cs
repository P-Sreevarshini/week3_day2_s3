public class OrderRepository
{
    private readonly List<Order> _orders = new List<Order>();

    public List<Order> GetOrders() => _orders;

    public Order GetOrder(int id) => _orders.FirstOrDefault(o => o.OrderId == id);

    public Order SaveOrder(Order order)
    {
        order.OrderId = _orders.Count + 1;
        _orders.Add(order);
        return order;
    }

    public Order UpdateOrder(int id, Order order)
    {
        var existingOrder = _orders.FirstOrDefault(o => o.OrderId == id);
        if (existingOrder != null)
        {
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.TotalAmount = order.TotalAmount;
        }
        return existingOrder;
    }

    public bool DeleteOrder(int id)
    {
        var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == id);
        if (orderToRemove != null)
        {
            _orders.Remove(orderToRemove);
            return true;
        }
        return false;
    }
}