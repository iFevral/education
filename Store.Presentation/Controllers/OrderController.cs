using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(ApplicationContext db,
                                IMapper mapper)
        {
            _orderService = new OrderService(db, mapper);
        }

        [Route("~/[controller]")]
        [HttpGet]
        public async Task<IActionResult> GetAll(string username, int startIndex, int quantity)
        { 
            OrderFilter orderFilter = new OrderFilter
            {
                Username = username
            };

            var orderModel = await _orderService.GetAll(orderFilter, startIndex, quantity);
            if (orderModel.Errors.Count > 0)
                return NotFound(orderModel.Errors);

            return Ok(orderModel.Orders);
        }

        [Route("~/[controller]/Find/{id}")]
        [HttpGet]
        public async Task<IActionResult> FindBy(int id, string username)
        {
            var orderModel = _orderService.FindById(id, username);
            if (orderModel.Errors.Count > 0)
                return NotFound(orderModel.Errors);

            return Ok(orderModel.Orders);
        }

        [Route("~/[controller]/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> FindById(int id)
        {
            var orderModel = await _orderService.FindById(id);
            if (orderModel.Errors.Count > 0)
                return NotFound(orderModel.Errors);

            return Ok(orderModel.Orders);
        }

        [Route("~/[controller]/Create")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> CreateOrder([FromBody]OrderInputData orderItem)
        {
            var orderModel = await _orderService.Create(orderItem);
            if (orderModel.Errors.Count > 0)
                return NotFound(orderModel.Errors);

            return Ok(orderModel.Orders);
        }

        [Route("~/[controller]/Update/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody]OrderInputData orderItem)
        {
            var orderModel = await _orderService.Update(id, orderItem);
            if (orderModel.Errors.Count > 0)
                return NotFound(orderModel.Errors);

            return Ok(orderModel.Orders);
        }

        [Route("~/[controller]/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderModel = await _orderService.Delete(id);
            if (orderModel.Errors.Count > 0)
                return NotFound(orderModel.Errors);

            return Ok(orderModel.Orders);
        }
    }
}