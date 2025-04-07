using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(Guid id, DateRange duration, Money priceForPeriod, Money cleaningFee, Money amenitiesUpCharge, Money totalPrice) : base(id)
    {
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
    }

    public Guid ApartmentId { get; private set; }
    public Guid UserId { get; private set; }
    public DateRange Duration { get; private set; }
    public Money PriceForPeriod { get; private set; }
    public Money CleaningFee { get; private set; }
    public Money AmenitiesUpCharge { get; private set; }
    public Money TotalPrice { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ConfirmedOnUtc { get; private set; }
    public DateTime? RejectedOnUtc { get; private set; }
    public DateTime? CompletedOnUtc { get; private set; }
    public DateTime? CancelledOnUtc { get; private set; }

    public static Booking Reserve(Guid apartmentId, Guid userId, DateRange duration, DateTime utcNow)
    {
        var booking = new Booking(Guid.NewGuid(), apartmentId, userId, duration);
         
    }
}