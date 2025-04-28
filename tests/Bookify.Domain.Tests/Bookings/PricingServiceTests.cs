using Bookify.Domain.Bookings;
using Bookify.Domain.Shared;
using Bookify.Domain.Tests.Apartments;
using Shouldly;

namespace Bookify.Domain.Tests.Bookings;

public class PricingServiceTests
{
    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice()
    {
        var price = new Money(10.0m, Currency.Usd);
        var period = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        var expectedTotalPrice = new Money(price.Amount * period.LengthInDays, price.Currency);
        var apartment = ApartmentData.Create(price);
        var pricingService = new PricingService();

        var pricingDetails = pricingService.CalculatePrice(apartment, period);

        pricingDetails.TotalPrice.ShouldBe(expectedTotalPrice);
    }

    [Fact]
    public void CalculatePrice_Should_ReturnCorrectTotalPrice_WhenCleaningFeeIsIncluded()
    {
        var price = new Money(10.0m, Currency.Usd);
        var cleaningFee = new Money(99.99m, Currency.Usd);
        var period = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        var expectedTotalPrice = new Money((price.Amount * period.LengthInDays) + cleaningFee.Amount, price.Currency);
        var apartment = ApartmentData.Create(price, cleaningFee);
        var pricingService = new PricingService();

        var pricingDetails = pricingService.CalculatePrice(apartment, period);

        pricingDetails.TotalPrice.ShouldBe(expectedTotalPrice);
    }
}