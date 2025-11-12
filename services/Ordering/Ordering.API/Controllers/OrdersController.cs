using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Controllers;
using Ordering.Application.Commands;
using Ordering.Application.Queries;

namespace Catalog.API.Controllers
{
    
    public class OrdersController : BaseApiController
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;
        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("{userName}", Name = "GetOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByUserName(string userName)
        {
            _logger.LogInformation("Getting orders for user: {UserName}", userName);
            // Implementation to get orders by user name
            var query = new GetOrderListQuery(userName);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CheckOutOrder (CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> UpdateCheckOutOrder(UpdateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return NoContent();

        }

        [HttpDelete(Name = "CheckoutOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> CheckOutOrder(DeleteOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return NoContent();

        }

    }
}
