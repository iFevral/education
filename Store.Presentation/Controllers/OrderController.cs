using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.Presentation.Helpers.Interface;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IJwtHelper _jwtHelper;

        public OrderController(IOrderService orderService,
                               IJwtHelper jwtHelper)
        {
            _orderService = orderService;
            _jwtHelper = jwtHelper;
        }

        [Route("~/[controller]s")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody]OrderFilterModel orderFilter)
        {
            var orderModel = await _orderService.GetAllAsync(orderFilter);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/[action]/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPost]
        public async Task<IActionResult> Get(int id)
        {
            var orderModel = await _orderService.FindByIdAsync(id);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/[action]")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPut]
        public async Task<IActionResult> Create([FromHeader]string authorization, [FromBody]OrderModelItem orderModelItem)
        {
            var token = authorization.Substring(7);
            orderModelItem.User.Id = _jwtHelper.GetUserIdFromToken(token);
            var orderModel = await _orderService.CreateAsync(orderModelItem);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/[action]")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]PaymentModelItem paymentModelItem)
        {
            var orderModel = await _orderService.UpdateAsync(paymentModelItem);

            return Ok(orderModel);
        }

        [Route("~/[controller]s/[action]/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var orderModel = await _orderService.DeleteAsync(id);

            return Ok(orderModel);
        }
    }
}