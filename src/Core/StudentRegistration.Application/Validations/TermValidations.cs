using FluentValidation;
using FluentValidation.Results;

namespace StudentRegistration.Application.Validations;

public class CreateTermCommandValidator : AbstractValidator<CreateTermCommand>
{
    public CreateTermCommandValidator()
    {
        RuleFor(c=>c.TermWeeklySlots).NotNull().WithMessage("Term Weekly Slots Cannot Be null");
        RuleFor(c => c.TermWeeklySlots.DailySlots).NotNull().WithMessage("Daily slots cannot be null")
            .NotEmpty().WithMessage("Daily slots cannot be empty")
            .When(c=>c.TermWeeklySlots != null);
    }

}
