using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Commands
{
    public class CreataProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponsDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CreataProductCommandHandler
            (IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository=productRepository;
            _mapper=mapper;
        }

        public async Task<ProductResponsDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.CreateProduct(product);
            
            return _mapper.Map<ProductResponsDto>(product);
        }
    }
}
