using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    /// <summary>
    /// A MediatR pipeline behavior that intercepts incoming requests and runs validation on them.
    /// It discovers all FluentValidation validators for the current request type and executes them.
    /// If any validation errors are found, it throws a ValidationException.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request being handled (e.g., a command or query).</typeparam>
    /// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the request by first performing validation and then, if successful, passing it to the next handler in the pipeline.
        /// </summary>
        /// <param name="request">The request to handle.</param>
        /// <param name="next">The delegate representing the next action in the pipeline.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The response from the next handler in the pipeline.</returns>
        /// <exception cref="ValidationException">Thrown if validation fails.</exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}