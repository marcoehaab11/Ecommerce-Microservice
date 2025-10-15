using Discount.Application.Query;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Discount.Application.Handler.Queries
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly ILogger<GetDiscountQueryHandler> _logger;
        private readonly IDiscountRepository _repository;
        public GetDiscountQueryHandler(
            ILogger<GetDiscountQueryHandler> logger,
            IDiscountRepository repository)
        {
            _logger = logger;
            _repository=repository;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                _logger.LogError("Discount with ProductName : {ProductName} not found", request.ProductName);
                throw new Exception($"Discount with ProductName : {request.ProductName} not found");
            }
            _logger.LogInformation("Discount with ProductName : {ProductName} found", request.ProductName);
            var couponModel = new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
            return couponModel ;
        }
    }
}
