using FluentValidation;
using System;

namespace Amazon.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .Matches(@"^[a-zA-Z0-9.]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Please enter a valid email address which shouldn't contain any special symbol other then  @ and . ");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("First name is required and must be at least 2 characters long")
                .MaximumLength(25)
                .WithMessage("First name is required and must be less than 25 characters long");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Last name is required and must be at least 2 characters long")
                .MaximumLength(25)
                .WithMessage("Last name is required and must be less than 25 characters long");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .Matches(@"^[6-9]+\d{9}$")
                .WithMessage("Please enter a valid phone number containing exactly 10 digits without any special characters or letters");


            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Address line 1 is required and must be at least 5 characters long")
                .MaximumLength(100)
                .WithMessage("Address line 1 is required and must be less than 100 characters long");

            RuleFor(x => x.AddressLine2)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Address line 2 is required and must be at least 5 characters long")
                .MaximumLength(100)
                .WithMessage("Address line 2 is required and must be less than 100 characters long"); 

            RuleFor(x => x.PinCode.ToString())
                .NotEmpty()
                .Must(BeAValidPinCode)
                .WithMessage("Please enter a valid PIN code");
        }

        private bool BeAValidPinCode(string pinCode)
        {
            // Custom logic to validate pin code
            return pinCode.Length == 6;
        }
    }
}

