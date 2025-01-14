using AutoMapper;
using Talabat.Core.Application.Abstraction.Common;
using Talabat.Core.Application.Abstraction.Models.Products;
using Talabat.Core.Application.Abstraction.Products;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Domain.Contract.Persistence;
using Talabat.Core.Domain.Entities.Product;
using Talabat.Core.Domain.Specifications.Products;

namespace Talabat.Core.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecificationParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex, specParams.Search);
            var products = await unitOfWork.GetRepo<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProduct = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFiltrationForCountSpec(specParams.BrandId, specParams.CategoryId, specParams.Search);
            var count = await unitOfWork.GetRepo<Product, int>().GetCountAsync(countSpec);

            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count) { Data = mappedProduct };
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
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
