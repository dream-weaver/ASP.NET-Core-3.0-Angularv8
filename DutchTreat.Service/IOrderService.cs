using DutchTreat.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutchTreat.Service
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrders(bool includeItems);
        IEnumerable<Order> GetOrdersByUser(string user, bool includeItems);
        Order GetOrderById(string user, int id);
        void AddEntity(Order order);
        void AddOrder(Order newOrder);
        bool Save();
    }
}
