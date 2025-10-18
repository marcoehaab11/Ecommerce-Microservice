using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
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
        private readonly DiscountGrpcService _discountGrpcService;
        public CreateShoppingCartCommandHandler(
            IMapper mapper,
            IBasketRepository basketRepository,
            DiscountGrpcService discountGrpcService)
        {
            _mapper=mapper;
            _basketRepository=basketRepository;
            _discountGrpcService=discountGrpcService;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                if (coupon is not null)
                {
                    item.Price -= coupon.Amount;
                }
            }

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
