using Talabat.Core.Domain.Entities.Product;

namespace Talabat.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            AddIncludes();
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

        #region Helper Methods
     
        private void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        } 
        
        #endregion
    }
}
