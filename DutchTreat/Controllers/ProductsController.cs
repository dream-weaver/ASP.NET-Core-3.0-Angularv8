using System;
using System.Collections.Generic;
using DutchTreat.Entity;
using DutchTreat.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_productService.GetProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products!");
            }           
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
