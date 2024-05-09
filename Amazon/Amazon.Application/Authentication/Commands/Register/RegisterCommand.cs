using Amazon.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Authentication.Commands.Register
{
    public record RegisterCommand
    (
          string FirstName,
          string LastName,
          string Email,
          string Password,
          string AddressLine1,
          string AddressLine2,
          int PinCode,
          string? PhoneNumber
    ) : IRequest<AuthenticationResult>;
}
