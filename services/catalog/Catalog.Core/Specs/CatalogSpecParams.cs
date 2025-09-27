using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specs
{
    public class CatalogSpecParams
    {
        private const int MaxPagaSize = 80;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPagaSize) ? MaxPagaSize : value;
        }
        public int PageIndex { get; set; } = 1;
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }




    }
}
