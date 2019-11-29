using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("~/[controller]s")]
        [HttpGet]
        public async Task<IActionResult> GetAll(string username, int startIndex, int quantity)
        { 
            OrderFilter orderFilter = new OrderFilter
            {
                Username = username
            };

            var orderModel = await _orderService.GetAllAsync(orderFilter, startIndex, quantity);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> FindById(int id)
        {
            var orderModel = await _orderService.FindByIdAsync(id);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }
            return Ok(orderModel);
        }

        [Route("~/[controller]s/Create")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> CreateOrder([FromBody]OrderModelItem OrderModelItem)
        {
            var orderModel = await _orderService.CreateAsync(OrderModelItem);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Update/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody]OrderModelItem OrderModelItem)
        {
            var orderModel = await _orderService.UpdateAsync(id, OrderModelItem);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderModel = await _orderService.DeleteAsync(id);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }
    }
}