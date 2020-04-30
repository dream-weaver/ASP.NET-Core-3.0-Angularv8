using DutchTreat.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutchTreat.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public StoreUser user { get; set; }
    }
}
