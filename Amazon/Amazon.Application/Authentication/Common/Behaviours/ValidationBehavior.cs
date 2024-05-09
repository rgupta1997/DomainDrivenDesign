using Amazon.Application.Authentication.Commands.Register;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Authentication.Common.Behaviours
{
    public class ValidationRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, AuthenticationResult>
    {
        private readonly IValidator<RegisterCommand> _validator;

        public ValidationRegisterCommandBehavior(IValidator<RegisterCommand> validator)
        {
            _validator = validator;
        }
        public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<AuthenticationResult> next)
        {

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var errors = validationResult.Errors.Select(validationFailure => validationFailure.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
            return null;
        }
    }
}
