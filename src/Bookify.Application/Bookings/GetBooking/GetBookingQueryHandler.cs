using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Bookings.GetBooking;

internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        
        const string sql = """
                           SELECT
                               Id AS Id,
                               ApartmentId AS ApartmentId,
                               UserId AS UserId,
                               Status AS Status,
                               CleaningFee_Amount AS PriceAmount,
                               PriceForPeriod_Currency AS PriceCurrency,
                               CleaningFee_Amount AS CleaningFeeAmount,
                               CleaningFee_Currency AS CleaningFeeCurrency,
                               AmenitiesUpCharge_Amount AS AmenitiesUpChargeAmount,
                               AmenitiesUpCharge_Currency AS AmenitiesUpChargeCurrency,
                               TotalPrice_Amount AS TotalPriceAmount,
                               TotalPrice_Currency AS TotalPriceCurrency,
                               Duration_Start AS DurationStart,
                               Duration_End AS DurationEnd,
                               CreatedOnUtc AS CreatedOnUtc
                           FROM bookings
                           WHERE id = @BookingId
                           """;

        var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(sql, new
        {
            request.BookingId
        });

        if (booking is null || booking.UserId != _userContext.UserId)
        {
            return Result.Failure<BookingResponse>(BookingErrors.NotFound);
        }

        return booking;
    }
}