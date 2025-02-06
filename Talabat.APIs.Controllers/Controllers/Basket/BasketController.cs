using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Common.Contracts.Infrastructure;
using Talabat.Shared.Models.Basket;

namespace Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController(IBasketService basketService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var basket = await basketService.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await basketService.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await basketService.DeleteCustomerBasketAsync(id);
        }
    }
}