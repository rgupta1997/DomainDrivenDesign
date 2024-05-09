using FluentValidation;

namespace Amazon.Validator
{
    public class MedicalDaignosisList : AbstractValidator<DiagnosisDetails>
    {
        public MedicalDaignosisList(MedicalDetails medicalDetails) 
        {
            RuleFor(x => x.DaignosisCode)
                .NotEmpty()
                .When(x => medicalDetails.LineOfTreatment.ToUpper() == "MM")
                .WithMessage("Daignosis Code is required");

            RuleFor(x => x.CPTCode)
                .NotEmpty()
                .When(x => medicalDetails.LineOfTreatment.ToUpper() == "SM")
                .WithMessage("CPT Code is required");
                
        }
    }
}
