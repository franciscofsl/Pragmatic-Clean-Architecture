using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);
    void Add(Booking booking);
    Task<Booking?> GetByIdAsync(Guid notificationBookingId, CancellationToken cancellationToken);
}