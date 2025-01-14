using AutoMapper;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contract.Persistence;

namespace Talabat.Core.Application.Services
{
    internal class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        public IProductService ProductService => _productService.Value;
    }
}
