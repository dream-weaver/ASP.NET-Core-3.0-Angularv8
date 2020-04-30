using DutchTreat.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutchTreat.Service
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public List<Product> GetProductsByCategory(string category);
    }
}
