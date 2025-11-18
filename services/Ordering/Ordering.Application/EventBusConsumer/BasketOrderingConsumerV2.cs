using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.EventBusConsumer
{
    public class BasketOrderingConsumerV2 :IConsumer<BasketCheckoutEventV2>
    {
        private readonly ILogger<BasketOrderingConsumerV2> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketOrderingConsumerV2 (ILogger<BasketOrderingConsumerV2> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEventV2> context)
        {
            using var scope = _logger.BeginScope("Consume BasketCheckoutEvent for {con} from v2",context.Message.CorrelationId);
            var cmd = _mapper.Map<CheckoutOrderCommandV2>(context.Message);
            var result = _mediator.Send(cmd);
            _logger.LogInformation("BasketCheckoutEvent consumed successfully for {con}", context.Message.CorrelationId);
        }
    }
}
