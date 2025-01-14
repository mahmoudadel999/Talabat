using Talabat.Core.Application.Abstraction.Common;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Products;

namespace Talabat.Core.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecificationParams specParams);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
