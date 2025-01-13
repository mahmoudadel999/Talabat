using AutoMapper;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Core.Domain.Specifications;
using Talabat.Core.Domain.Specifications.Products;

namespace Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await unitOfWork.GetRepo<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProduct = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return mappedProduct;
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var product = await unitOfWork.GetRepo<Product, int>().GetWithSpecAsync(spec);
            var mappedProduct = mapper.Map<ProductToReturnDto>(product);
            return mappedProduct;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepo<ProductBrand, int>().GetAllAsync();
            var mappedBrands = mapper.Map<IEnumerable<BrandDto>>(brands);
            return mappedBrands;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.GetRepo<ProductCategory, int>().GetAllAsync();
            var mappedCategory = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return mappedCategory;
        }
    }
}
