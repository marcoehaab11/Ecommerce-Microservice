using Discount.Application.Command;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handler.Commands
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly ILogger<CreateDiscountCommandHandler> _logger;
        private readonly IDiscountRepository _repository;

        public CreateDiscountCommandHandler(
            IDiscountRepository repository,
            ILogger<CreateDiscountCommandHandler> logger)
        {
            _repository=repository;
            _logger=logger;
        }

        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon= new Coupon
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount
            };

            await _repository.CreateDiscount(coupon);
           _logger.LogInformation("Discount with ProductName : {ProductName} created", request.ProductName);

            var couponModel = new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
            return couponModel;
        }
    }
}
