using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository=productRepository;
            _mapper=mapper;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeleteProduct(request.Id);
            return true;
        }
    }
}
