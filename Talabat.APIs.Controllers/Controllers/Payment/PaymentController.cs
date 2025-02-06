using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using Talabat.Shared.Models.Basket;

namespace Talabat.APIs.Controllers.Controllers.Payment
{
    public class PaymentController(IPaymentService paymentService) : BaseApiController
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntetnt(string basketId)
        {
            var result = await paymentService.CreateOrUpdatePaymnetIntent(basketId);
            return Ok(result);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            await paymentService.UpdateOrderStatus(json, Request.Headers["Stripe_Signature"]!);

            return Ok();
        }

    }
}
