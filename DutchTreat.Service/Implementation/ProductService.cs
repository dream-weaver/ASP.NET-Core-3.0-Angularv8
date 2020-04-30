using DutchTreat.Data;
using DutchTreat.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DutchTreat.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly DutchTreatDBContext _dBContext;
        private readonly ILogger<ProductService> _logger;
        public ProductService(DutchTreatDBContext dBContext, ILogger<ProductService> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }
        public List<Product> GetProducts()
        {
            try
            {
                var results = _dBContext.Products.OrderBy(p => p.Title).ToList();
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to load all Products:{ex}");
                return null;
            }          
        }

        public List<Product> GetProductsByCategory(string category)
        {
            try
            {
                var results = _dBContext.Products.OrderBy(p => p.Category == category).ToList();
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to load Products by Category: {ex} ");
                return null;
            }            
            //var query= from p in _dBContext.Products orderby (p.Category == category) select p;
        }
    }
}
