using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public  class GetAllProductByTypeNameQueryHandler:IRequestHandler<GetAllProductByTypeNameQuery, IList<ProductResponsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public GetAllProductByTypeNameQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponsDto>> Handle(GetAllProductByTypeNameQuery request, CancellationToken cancellationToken)
        {
            var productList =await _productRepository.GetProductByTypeName(request.Name);
            return _mapper.Map<IList<ProductResponsDto>>(productList);
        }
    }
}
