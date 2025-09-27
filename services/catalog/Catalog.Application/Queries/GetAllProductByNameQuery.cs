using Catalog.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetAllProductByTypeNameQuery:IRequest<IList<ProductResponsDto>>
    {
        public string Name { get; set; }
        public GetAllProductByTypeNameQuery(string name)
        {
            Name = name;
        }
    }
}
