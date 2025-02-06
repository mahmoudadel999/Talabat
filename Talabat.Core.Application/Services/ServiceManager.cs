using AutoMapper;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Application.Abstraction.Services.Product;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contract.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, Func<IAuthService> authServiceFactory, Func<IOrderService> orderServiceFactory) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        private readonly Lazy<IAuthService> _authService = new Lazy<IAuthService>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);

        private readonly Lazy<IOrderService> _orderService = new Lazy<IOrderService>(orderServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);

        public IProductService ProductService => _productService.Value;

        public IAuthService AuthService => _authService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
