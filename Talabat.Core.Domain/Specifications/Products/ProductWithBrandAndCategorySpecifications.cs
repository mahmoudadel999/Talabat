using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int pageSize, int pageIndex, string? search)
            : base(
                P =>
                (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                &&
                (!brandId.HasValue || P.BrandId == brandId.Value)
                &&
                (!categoryId.HasValue || P.CategoryId == categoryId.Value)
            )
        {
            AddIncludes();

            AddOrderBy(P => P.Name);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(P => P.Name);
                        break;

                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            ApplyPagination(pageSize * (pageIndex - 1), pageSize);
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

        #region Helper Methods

        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

        #endregion
    }
}
