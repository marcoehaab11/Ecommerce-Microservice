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
    public class BasketOrderingConsumer :IConsumer<BasketCheckoutEvent>
    {
        private readonly ILogger<BasketOrderingConsumer> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketOrderingConsumer(ILogger<BasketOrderingConsumer> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            using var scope = _logger.BeginScope("Consume BasketCheckoutEvent for {con}",context.Message.CorrelationId);
            var cmd = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = _mediator.Send(cmd);
            _logger.LogInformation("BasketCheckoutEvent consumed successfully for {con}", context.Message.CorrelationId);
        }
    }
}
