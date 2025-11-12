using AutoMapper;
using MediatR;
using Ordering.Application.Queries;
using Ordering.Application.Respones;
using Ordering.Core.Reposiotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handler.Query
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListQueryHandler(
            IOrderRepository orderRepository,
            IMapper mapper
            )
        {
            _mapper=mapper;
            _orderRepository=orderRepository;
        }

        public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserNameAsync(request.UserName);
            return _mapper.Map<List<OrderResponse>>(orderList);
        }
    }
}
