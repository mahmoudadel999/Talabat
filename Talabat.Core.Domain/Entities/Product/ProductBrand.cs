﻿namespace Talabat.Core.Domain.Entities.Product
{
    public class ProductBrand : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }

        
    }
}
