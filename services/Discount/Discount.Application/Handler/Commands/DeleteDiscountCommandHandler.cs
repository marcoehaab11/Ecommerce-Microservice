using Discount.Application.Command;
using Discount.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handler.Commands
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly ILogger<DeleteDiscountCommandHandler> _logger;
        private readonly IDiscountRepository _repository;   
        public DeleteDiscountCommandHandler(
            IDiscountRepository repository,
            ILogger<DeleteDiscountCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            if(await _repository.DeleteDiscount(request.ProductName))
                 return true;

            return false;
        }
    }
}
