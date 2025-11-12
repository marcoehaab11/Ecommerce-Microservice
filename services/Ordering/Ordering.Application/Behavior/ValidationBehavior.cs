using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behavior
{
    //this method run befor the actual handler is executed
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {   
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                //will run all validation rules one by one and collect the errors
                var validationResults = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(failure => failure != null)
                    .ToList();
                if (validationResults.Count != 0)
                {
                    throw new ValidationException(validationResults);
                }
            }
            return await next();

        }
    }
}
