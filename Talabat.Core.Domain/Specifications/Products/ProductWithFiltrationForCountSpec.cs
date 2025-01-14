using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithFiltrationForCountSpec : BaseSpecifications<Product, int>
    {
        public ProductWithFiltrationForCountSpec(int? brandId, int? categoryId, string? search)
            : base(
            P =>
                (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                &&
                (!brandId.HasValue || P.BrandId == brandId.Value)
                &&
                (!categoryId.HasValue || P.CategoryId == categoryId.Value))
        {

        }
    }
}
