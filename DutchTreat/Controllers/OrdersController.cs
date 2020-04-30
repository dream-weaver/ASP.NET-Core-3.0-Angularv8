using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Entities;
using DutchTreat.Entity;
using DutchTreat.Service;
using DutchTreat.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _orderService = orderService;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: api/Orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;
                var results = _orderService.GetOrdersByUser(username, includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_orderService.GetOrders(includeItems)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders!");
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id:int}")]
        public ActionResult<Order> Get(int id)
        {
            try
            {
                var order = _orderService.GetOrderById(User.Identity.Name, id);
                if (order!=null)
                {
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                }else{
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order by id: {ex}");
                return BadRequest("Failed to get order by id!");
            }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]OrderViewModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(order);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.user = currentUser;
                    _orderService.AddOrder(newOrder);
                    if (_orderService.Save())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }else{
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new order: {ex}");              
            }

            return BadRequest("Failed to save new order!");
        }

        //// PUT: api/Orders/5
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
