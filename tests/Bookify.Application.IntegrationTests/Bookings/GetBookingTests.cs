using Bookify.Application.Bookings.GetBooking;
using Bookify.Application.IntegrationTests.Infrastructure;
using Bookify.Domain.Bookings;
using Shouldly;

namespace Bookify.Application.IntegrationTests.Bookings;

public class GetBookingTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly Guid BookingId = Guid.NewGuid();

    [Fact]
    public async Task GetBooking_ShouldReturnFailure_WhenBookingIsNotFound()
    {
        var query = new GetBookingQuery(BookingId);

        var result = await Sender.Send(query);

        result.Error.ShouldBe(BookingErrors.NotFound);
    }
}