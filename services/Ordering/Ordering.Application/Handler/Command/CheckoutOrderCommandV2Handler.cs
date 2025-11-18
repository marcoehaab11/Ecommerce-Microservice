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
    public class CheckoutOrderCommandV2Handler : IRequestHandler<CheckoutOrderCommandV2, int>
    {
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CheckoutOrderCommandV2Handler(
            IMapper mapper,
            IOrderRepository orderRepository,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _mapper=mapper;
            _orderRepository=orderRepository;
            _logger=logger;
        }

        public async Task<int> Handle(CheckoutOrderCommandV2 request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var geneatedOrder = await _orderRepository.AddAsync(orderEntity);
            _logger.LogInformation($"Order {geneatedOrder.Id} is successfully created.");
            return geneatedOrder.Id;

        }
    }
}
