using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Helpers;

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
        public async Task<IActionResult> GetAllAsync([FromBody]OrderFilterModel orderFilter)
        {
            var orderModel = await _orderService.GetAllAsync(orderFilter);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Count")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> GetNumberAsync()
        {
            int counter = await _orderService.GetNumberOfOrders();

            return Ok(counter);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPost]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var orderModel = await _orderService.FindByIdAsync(id);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Create")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPut]
        public async Task<IActionResult> CreateAsync([FromHeader]string Authorization, [FromBody]OrderModelItem orderModelItem)
        {
            var token = Authorization.Substring(7);
            orderModelItem.User.Id = JwtHelper.GetUserIdFromToken(token);
            var orderModel = await _orderService.CreateAsync(orderModelItem);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Update/")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]PaymentModelItem paymentModelItem)
        {
            var orderModel = await _orderService.UpdateAsync(paymentModelItem);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var orderModel = await _orderService.DeleteAsync(id);

            return Ok(orderModel);
        }
    }
}