namespace Bookify.Api.Controllers.Apartments;

public sealed record ReserveBookingRequest(Guid ApartmentId, Guid UserId, DateOnly StartDate, DateOnly EndDate);