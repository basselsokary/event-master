using EventMaster.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static EventMaster.Domain.Constants.DomainConstants.Money;

namespace EventMaster.Infrastructure.Context.Configurations;

public class MoneyConfiguration
{
    public static void Configure<T>(OwnedNavigationBuilder<T, Money> ownerBuilder)
        where T : class
    {
        ownerBuilder.Property(m => m.Amount)
            .HasColumnName(nameof(Money.Amount))
            .HasPrecision(Precision, Scale)
            .IsRequired();

        ownerBuilder.Property(m => m.Currency)
            .HasColumnName(nameof(Money.Currency))
            .HasMaxLength(MaxCurrencyLength)
            .IsRequired();
    }
}
