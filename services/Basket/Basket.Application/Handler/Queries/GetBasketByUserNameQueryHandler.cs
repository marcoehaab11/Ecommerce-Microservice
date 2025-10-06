using AutoMapper;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handler.Queries
{
    public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _repository;

        private readonly IMapper _mapper;
        public GetBasketByUserNameQueryHandler(
            IBasketRepository repository,
            IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetBasket(request.UserName);
            var response = _mapper.Map<ShoppingCartResponse>(basket);
            return response;
        }
    }
}
