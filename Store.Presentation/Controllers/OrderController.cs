using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("~/[controller]s")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody]OrderFilterModel orderFilter)
        {
            var orderModel = await _orderService.GetAllAsync(orderFilter);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [Authorize(Roles = Constants.RoleNames.Client)]
        [HttpPost]
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
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> CreateOrder([FromBody]OrderModelItem orderModelItem)
        {
            var orderModel = await _orderService.CreateAsync(orderModelItem);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Update/")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody]PaymentModelItem paymentModelItem)
        {
            var orderModel = await _orderService.UpdateAsync(paymentModelItem);
            if (orderModel.Errors.Count > 0)
            {
                return NotFound(orderModel);
            }

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
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