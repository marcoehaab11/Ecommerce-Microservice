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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponsDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(
            IProductRepository productRepository, 
            IMapper mapper)
        {
            _productRepository=productRepository;
            _mapper=mapper;
        }

        public async Task<ProductResponsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductById(request.Id);
            return _mapper.Map<ProductResponsDto>(product);
        }
    }
}
