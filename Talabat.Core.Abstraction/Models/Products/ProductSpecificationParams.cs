using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Products
{
    public class ProductSpecificationParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxSizePage = 10;
        private int pageSize = 5;
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToUpper(); }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxSizePage ? MaxSizePage : value; }
        }
    }
}
