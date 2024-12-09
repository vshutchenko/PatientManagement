using FluentValidation;
using PatientManagement.Api.ViewModels.Patient;
using PatientManagement.Services.Patient;

namespace PatientManagement.Api.Validators;

public class PatientViewModelValidator : AbstractValidator<PatientViewModel>
{
    public PatientViewModelValidator()
    {
        RuleFor(x => x.Name.Family)
            .NotEmpty().WithMessage("Family name is required.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required.");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Birth date cannot be in the future.");

        RuleFor(p => p.Gender)
            .Must(BeValidGender)
            .WithMessage($"Gender must be one of the following: " +
            $"{string.Join(", ", Enum.GetNames(typeof(Gender)).Select(g => g.ToLower()))}");    
    }

    private bool BeValidGender(string gender)
    {
        return Enum.GetNames(typeof(Gender))
            .Select(g => g.ToLower())
            .Contains(gender.ToLower());
    }
}
