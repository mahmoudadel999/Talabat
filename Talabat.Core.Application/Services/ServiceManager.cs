using AutoMapper;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Product;
using Talabat.Core.Application.Services.Auth;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contract.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, Func<IBasketService> basketServiceFactory, Func<IAuthService> authServiceFactory) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(basketServiceFactory);

        private readonly Lazy<IAuthService> _authService = new Lazy<IAuthService>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;
    }
}
