using Amazon.Validator;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Amazon.Application.Authentication.Commands.Register
{
    public class ConditionalValidation : AbstractValidator<ClaimSearch>
    {
        public ConditionalValidation()
        {

            RuleForEach(x => x.items)
            .SetValidator(new DocumentListValidation());

            RuleFor(x => x.ClaimNumber.ToString())
    .Empty()
    .When(x => !string.IsNullOrEmpty(x.OhidNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
    .WithMessage("ClaimNumber should not be provided when OhidNumber or PolicyNumber is present");

            RuleFor(x => x.ClaimNumber.ToString())
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.InsuredName) && string.IsNullOrEmpty(x.DOB)
                            && string.IsNullOrEmpty(x.HospitalId.ToString()) && string.IsNullOrEmpty(x.HospitalName)
                            && string.IsNullOrEmpty(x.MobileNumber.ToString()) && string.IsNullOrEmpty(x.EmailId)
                            && string.IsNullOrEmpty(x.OhidNumber.ToString()) && string.IsNullOrEmpty(x.PolicyNumber.ToString())) 
                .WithMessage("ClaimNumber should not be Empty");

            RuleFor(x => x.OhidNumber.ToString())
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("OhidNumber should not be provided when ClaimNumber or PolicyNumber is present");

            RuleFor(x => x.OhidNumber.ToString())
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.InsuredName) && string.IsNullOrEmpty(x.DOB)
                            && string.IsNullOrEmpty(x.HospitalId.ToString()) && string.IsNullOrEmpty(x.HospitalName)
                            && string.IsNullOrEmpty(x.MobileNumber.ToString()) && string.IsNullOrEmpty(x.EmailId)
                            && string.IsNullOrEmpty(x.ClaimNumber.ToString()) && string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("OhidNumber should not be Empty");

            RuleFor(x => x.PolicyNumber.ToString())
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.OhidNumber.ToString()))
                .WithMessage("PolicyNumber should not be provided when ClaimNumber or OhidNumber is present");

            RuleFor(x => x.PolicyNumber.ToString())
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.InsuredName) && string.IsNullOrEmpty(x.DOB)
                            && string.IsNullOrEmpty(x.HospitalId.ToString()) && string.IsNullOrEmpty(x.HospitalName)
                            && string.IsNullOrEmpty(x.MobileNumber.ToString()) && string.IsNullOrEmpty(x.EmailId)
                            && string.IsNullOrEmpty(x.OhidNumber.ToString()) && string.IsNullOrEmpty(x.ClaimNumber.ToString()))
                .WithMessage("PolicyNumber should not be Empty");

            RuleFor(x => x.DOB)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.OhidNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("DOB should not be provided when ClaimNumber, OhidNumber, or PolicyNumber is present");

            RuleFor(x => x.DOB)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.InsuredName) && string.IsNullOrEmpty(x.PolicyNumber.ToString()) && string.IsNullOrEmpty(x.OhidNumber.ToString()) && string.IsNullOrEmpty(x.ClaimNumber.ToString()))
                .WithMessage("DOB should be provided when Insured Name is provided");

            RuleFor(x => x.InsuredName)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.OhidNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("Insured Name should not be provided when ClaimNumber, OhidNumber, or PolicyNumber is present");

            RuleFor(x => x.InsuredName)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.DOB) || !string.IsNullOrEmpty(x.HospitalId.ToString()) || !string.IsNullOrEmpty(x.HospitalName)
                            || !string.IsNullOrEmpty(x.MobileNumber.ToString()) || !string.IsNullOrEmpty(x.EmailId))
                .WithMessage("Insured Name should be provided");

            RuleFor(x => x.MobileNumber)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.OhidNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("Mobile Number should not be provided when ClaimNumber, OhidNumber, or PolicyNumber is present");

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.HospitalId.ToString()) && string.IsNullOrEmpty(x.PolicyNumber.ToString()) && string.IsNullOrEmpty(x.OhidNumber.ToString()) && string.IsNullOrEmpty(x.ClaimNumber.ToString()))
                .WithMessage("Mobile Number should be provided when Hospital Id is provided");

            RuleFor(x => x.EmailId)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.OhidNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("Email Id should not be provided when ClaimNumber, OhidNumber, or PolicyNumber is present");

            RuleFor(x => x.EmailId)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.HospitalId.ToString()) && string.IsNullOrEmpty(x.PolicyNumber.ToString()) && string.IsNullOrEmpty(x.OhidNumber.ToString()) && string.IsNullOrEmpty(x.ClaimNumber.ToString()))
                .WithMessage("Email Id should be provided when Hospital Id is provided");

            RuleFor(x => x.HospitalName)
                .Empty()
                .When(x => !string.IsNullOrEmpty(x.ClaimNumber.ToString()) || !string.IsNullOrEmpty(x.OhidNumber.ToString()) || !string.IsNullOrEmpty(x.PolicyNumber.ToString()))
                .WithMessage("Hospital Name should not be provided when ClaimNumber, OhidNumber, or PolicyNumber is present");

            RuleFor(x => x.HospitalName)
                .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.EmailId) || !string.IsNullOrEmpty(x.MobileNumber.ToString()) || !string.IsNullOrEmpty(x.InsuredName) && string.IsNullOrEmpty(x.DOB)
                && string.IsNullOrEmpty(x.PolicyNumber.ToString()) && string.IsNullOrEmpty(x.OhidNumber.ToString()) && string.IsNullOrEmpty(x.ClaimNumber.ToString()))
                .WithMessage("Hospital Name should be provided");

        }

    }
}

