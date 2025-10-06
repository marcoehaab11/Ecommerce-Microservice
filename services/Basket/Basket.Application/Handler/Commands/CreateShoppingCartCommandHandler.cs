using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.Commands
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        IBasketRepository _basketRepository;
        IMapper _mapper;

        public CreateShoppingCartCommandHandler(
            IMapper mapper,
            IBasketRepository basketRepository)
        {
            _mapper=mapper;
            _basketRepository=basketRepository;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart =await _basketRepository.UpdateBasket(new Core.Entites.ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });
             var response = _mapper.Map<ShoppingCartResponse>(shoppingCart);
            return response;

        }
    }
}
