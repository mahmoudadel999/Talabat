using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Exceptions;
using Talabat.Core.Domain.Contract.Infrastructure;
using Talabat.Core.Domain.Entities.Basket;

namespace Talabat.Core.Application.Services.Basket
{
    internal class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetAsync(basketId);
            if (basket is null)
                throw new NotFoundException(nameof(CustomerBasket), basketId);
            return _mapper.Map<CustomerBasketDto>(basket);
        }

        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basketDto);
            var timeToLive = TimeSpan.FromDays(double.Parse(_configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));

            var updatedBasket = await _basketRepository.UpdateAsync(mappedBasket, timeToLive);
            if (updatedBasket is null)
                throw new BadRequestException("Can't update, there is a problem with this basket");
            return basketDto;
        }

        public async Task DeleteCustomerBasketAsync(string basketId)
        {
            var deleted = await _basketRepository.DeleteAsync(basketId);
            if (!deleted) throw new BadRequestException("Unable to delete this basket");
        }


    }
}
