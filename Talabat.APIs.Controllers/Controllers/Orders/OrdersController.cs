using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Order;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderToCreateDto order)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.CreateOrderAsync(buyerEmail!, order);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrders()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrdersAsync(buyerEmail!);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrder(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await serviceManager.OrderService.GetOrderByIdAsync(buyerEmail!, id);

            return Ok(result);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliverMethodDto>>> GetDeliveryMethod()
        {
            var result = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(result);
        }
    }
}
