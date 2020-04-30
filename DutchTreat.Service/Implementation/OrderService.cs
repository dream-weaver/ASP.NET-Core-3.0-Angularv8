using DutchTreat.Data;
using DutchTreat.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DutchTreat.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly DutchTreatDBContext _dBContext;
        private readonly ILogger<OrderService> _logger;
        public OrderService(DutchTreatDBContext dBContext, ILogger<OrderService> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }
        public IEnumerable<Order> GetOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _dBContext.Orders
                        .Include(i => i.Items)
                        .ThenInclude(p => p.Product).ToList();
            }else
            {
                return _dBContext.Orders.ToList();
            }         
        }
        public IEnumerable<Order> GetOrdersByUser(string user, bool includeItems)
        {
            if (includeItems)
            {
                return _dBContext.Orders
                        .Where(o=>o.user.UserName == user)
                        .Include(i => i.Items)
                        .ThenInclude(p => p.Product).ToList();
            }
            else
            {
                return _dBContext.Orders
                        .Where(o => o.user.UserName == user)
                        .ToList();
            }
        }

        Order IOrderService.GetOrderById(string user, int id)
        {
            return _dBContext.Orders
                    .Where(o=>o.Id == id && o.user.UserName == user)
                    .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefault();
        }

        public void AddEntity(Order order)
        {
            _dBContext.Orders.Add(order);
        }
        public bool Save()
        {
            _dBContext.SaveChanges();
            return true;
        }

        public void AddOrder(Order newOrder)
        {
            // convert new products to look up of products
            foreach (var item in newOrder.Items)
            {
                item.Product = _dBContext.Products.Find(item.Product.Id);

            }
            AddEntity(newOrder);
        }
    }
}
