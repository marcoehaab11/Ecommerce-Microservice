using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpGet]
        [Route("[action]/{username}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBasket(CreateShoppingCartCommand command)
        {
            var basket = await _mediator.Send(command);
            return Ok(basket);
        }

        [HttpDelete("DeleteBasket/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var command = new DeleteShoppingCartCommand(userName);
            return Ok ( await _mediator.Send(command));
        }
    }
}
