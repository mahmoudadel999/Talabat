using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Application.Abstraction.Services.Product;

namespace Talabat.Core.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IAuthService AuthService { get; }
        public IOrderService OrderService { get; }
    }
}
