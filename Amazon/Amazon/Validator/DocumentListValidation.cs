using FluentValidation;

namespace Amazon.Validator
{
    public class DocumentListValidation : AbstractValidator<Item>
    {
        public DocumentListValidation()
        {
            RuleFor(x => x.DocumentName)
                .NotEmpty()
                .WithMessage("DocumentName is required")
                 .Matches(@"^[a-zA-Z\s]+$")
                .WithMessage("DocumentName should only contain aplhabet and spaces");

            RuleFor(x => x.DocumentNumber)
                .NotEmpty()
                .WithMessage("DocumentNumber is required")
                .Matches(@"^[a-zA-Z0-9]+$")
                .WithMessage("DocumentName should be Alpha-Numeric"); ;
        }
    }
}
