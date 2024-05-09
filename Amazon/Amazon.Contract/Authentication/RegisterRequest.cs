using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Contract.Authentication
{
    public record RegisterRequest(
          string FirstName,
          string LastName,
          string Email,
          string Password,
          string AddressLine1,
          string AddressLine2,
          int PinCode,
          string? PhoneNumber 
    );
}
