using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Shared;
using Bookify.Domain.Tests.Apartments;
using Bookify.Domain.Tests.Infrastructure;
using Bookify.Domain.Tests.Users;
using Bookify.Domain.Users;
using Shouldly;

namespace Bookify.Domain.Tests.Bookings;

public class BookingTests : BaseTest
{
    [Fact]
    public void Reserve_Should_RaiseBookingReservedDomainEvent()
    {
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);
        var price = new Money(10.0m, Currency.Usd);
        var duration = DateRange.Create(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));
        var apartment = ApartmentData.Create(price);
        var pricingService = new PricingService();

        var booking = Booking.Reserve(apartment, user.Id, duration, DateTime.UtcNow, pricingService);

        var bookingReservedDomainEvent = AssertDomainEventWasPublished<BookingReservedDomainEvent>(booking);

        bookingReservedDomainEvent.BookingId.ShouldBe(booking.Id);
    }
}
