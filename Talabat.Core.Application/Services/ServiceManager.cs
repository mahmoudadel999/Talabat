using AutoMapper;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contract.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, Func<IBasketService> basketServiceFactory) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(basketServiceFactory);


        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
