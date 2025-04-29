using FluentValidation;

namespace Bookify.Application.Bookings.ReserveBooking;

internal class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(_ => _.UserId).NotEmpty();
        RuleFor(_ => _.ApartmentId).NotEmpty();
        RuleFor(_ => _.StartDate).LessThan(_ => _.EndDate);
    }
}