using AutoMapper;
using Catalog.Application.Commands;
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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {   
        private readonly IProductRepository _productRepository; 
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler
            (IProductRepository productRepository,
             IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper=mapper;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var ProductEntity =await _productRepository.UpdateProduct(new Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Summary = request.Summary,
                Price = request.Price,
                ImageFile = request.ImageFile,
                productType = request.productType,
                productBrand = request.productBrand
            });
            return true;
        }
    }
}
