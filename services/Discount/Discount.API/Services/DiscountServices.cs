using Discount.Application.Command;
using Discount.Application.Query;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountServices : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        public DiscountServices(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext serverCall)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var result = await _mediator.Send(query);

            return result;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon;
            var query = new CreateDiscountCommand()
            {
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName
            };
            var result = await _mediator.Send(query);
            return result;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon;
            var query = new UpdateDiscountCommand()
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName
            };
            var result = await _mediator.Send(query);
            return result;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var query = new DeleteDiscountCommand(request.ProductName);
            var result = await _mediator.Send(query);
            var response = new DeleteDiscountResponse
            {
                Success = result
            };
            return response;


        }
    }

}
