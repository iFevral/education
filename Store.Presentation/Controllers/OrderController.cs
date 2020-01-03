using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Common.Constants;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities.Enums;
using Store.Presentation.Helpers.Interface;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
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

        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromHeader]string authorization, [FromBody]OrderFilterModel orderFilter)
        {            
            string userRole = _jwtHelper.GetUserRoleFromToken(authorization);
            if (userRole.Equals(Enums.Role.RoleName.Client.ToString()))
            {
                orderFilter.UserId = _jwtHelper.GetUserIdFromToken(authorization);
            }

            var orderModel = await _orderService.GetAllAsync(orderFilter);
            return Ok(orderModel);
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromHeader]string authorization, [FromBody]OrderModelItem orderModelItem)
        {
            orderModelItem.User.Id = _jwtHelper.GetUserIdFromToken(authorization);
            var orderModel = await _orderService.CreateAsync(orderModelItem);

            return Ok(orderModel);
        }


        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]PaymentModelItem paymentModelItem)
        {
            var orderModel = await _orderService.UpdateAsync(paymentModelItem);

            return Ok(orderModel);
        }
    }
}