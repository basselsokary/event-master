using FluentValidation;
using static EventMaster.Domain.Constants.DomainConstants.Money;

namespace EventMaster.Application.EntityRequests.Common.Money;

public class MoneyDtoValidator : AbstractValidator<MoneyDto>
{
    public MoneyDtoValidator()
    {
        RuleFor(m => m.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.");

        RuleFor(m => m.Currency)
            .MaximumLength(MaxCurrencyLength)
            .WithMessage($"Currency must not exceed {MaxCurrencyLength} characters.")
            .When(m => !string.IsNullOrWhiteSpace(m.Currency));

    }
}