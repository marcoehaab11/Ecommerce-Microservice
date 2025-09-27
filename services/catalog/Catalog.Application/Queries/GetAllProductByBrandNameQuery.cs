using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetAllProductByBrandNameQuery :IRequest<IList<ProductResponsDto>>
    {
        public string BrandName { get; set; }
        public GetAllProductByBrandNameQuery(string brandName)
        {
            BrandName = brandName;
        }
    }
}
