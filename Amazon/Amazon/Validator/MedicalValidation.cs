using FluentValidation;

namespace Amazon.Validator
{
    public class MedicalValidation : AbstractValidator<MedicalDetails>
    {
        public MedicalValidation() 
        {
                RuleForEach(x => x.diagnosis)
                    .SetValidator(x => new MedicalDaignosisList(x));
        }
    }
}
