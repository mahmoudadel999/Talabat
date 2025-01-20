using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Product;

namespace Talabat.Core.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthService AuthService { get; }
    }
}
