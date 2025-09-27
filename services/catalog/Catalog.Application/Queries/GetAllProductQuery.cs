using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetAllProductQuery :IRequest<Pagination<ProductResponsDto>>
    {
        public CatalogSpecParams  Spec { get; set; }
        public GetAllProductQuery(CatalogSpecParams spec)
        {
            Spec = spec;
        }
    }
}
