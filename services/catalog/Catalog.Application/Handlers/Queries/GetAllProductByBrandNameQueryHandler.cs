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
    public class GetAllProductByBrandNameQueryHandler : IRequestHandler<GetAllProductByBrandNameQuery, IList<ProductResponsDto>>
    {   
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductByBrandNameQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IList<ProductResponsDto>> Handle(GetAllProductByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var productList=await _productRepository.GetProductByBrandName(request.BrandName);
            return _mapper.Map<IList<ProductResponsDto>>(productList);
        }
    }
}
