using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllTypesQueryHandler:IRequestHandler<GetAllTypesQuery, IList<TypeResponsDto>>
    {
        private readonly IProductType _productType;
        private readonly IMapper _mapper;
        public GetAllTypesQueryHandler(IProductType productType, IMapper mapper)
        {
            _productType = productType;
            _mapper = mapper;
        }

        public async Task<IList<TypeResponsDto>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _productType.GetProductTypes();
            return _mapper.Map<IList<TypeResponsDto>>(types);

        }
    }

}
