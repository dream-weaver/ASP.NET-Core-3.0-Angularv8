using AutoMapper;
using DutchTreat.Entity;
using DutchTreat.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DutchTreat.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace DutchTreat.Controllers
{
    [Route("api/orders/{orderid}/items")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : ControllerBase
    {
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderItemsController(ILogger<OrderItemsController> logger, IOrderService orderService, IMapper mapper)
        {
            _logger = logger;
            _orderService = orderService;
            _mapper = mapper;
        }
        // GET: api/OrderItems
        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _orderService.GetOrderById(User.Identity.Name, orderId);
            if(order!=null){
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }else{
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _orderService.GetOrderById(User.Identity.Name, orderId);
            if(order!=null){
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item!=null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }                
            }
            return NotFound();
        }

        //// GET: api/OrderItems/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/OrderItems
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/OrderItems/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
