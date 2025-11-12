using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Core.Common;
using Ordering.Core.Reposiotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handler.Command
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(
            IMapper mapper,
            IOrderRepository orderRepository,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _mapper=mapper;
            _orderRepository=orderRepository;
            _logger=logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);

            await _orderRepository.UpdateAsync(orderEntity);

            _logger.LogInformation($"Order {orderEntity.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
