using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Reposiotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handler.Command
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger, IMapper mapper, IOrderRepository orderRepository)
        {
            _logger=logger;
            _mapper=mapper;
            _orderRepository=orderRepository;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete =await _orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
            {
                _logger.LogError($"Order with Id: {request.Id} not found.");
                throw new KeyNotFoundException($"Order with Id: {request.Id} not found.");
            }
            _orderRepository.DeleteAsync(orderToDelete).Wait();
            _logger.LogInformation($"Order with Id: {request.Id} deleted successfully.");
            return Unit.Value;
        }
    }
}
