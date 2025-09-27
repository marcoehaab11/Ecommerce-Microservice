using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Pagination<ProductResponsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetAllProductQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper=mapper;
            _productRepository=productRepository;
        }


        public async Task<Pagination<ProductResponsDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProduct(request.Spec);
            return _mapper.Map<Pagination<ProductResponsDto>>(products);
        }
    }
}
