using FluentValidation;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderCommandV2Validator : AbstractValidator<CheckoutOrderCommandV2>
    {

        public CheckoutOrderCommandV2Validator()
        {
                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("UserName is required.")
                    .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");
                
           RuleFor(x => x.TotalPrice)
                .GreaterThan(-1)
                .WithErrorCode("TotalPrice must be greater than or equal to 0.");

        }
    }
}
