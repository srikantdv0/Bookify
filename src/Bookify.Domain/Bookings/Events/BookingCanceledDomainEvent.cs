namespace Bookify.Domain.Bookings.Events;
using Bookify.Domain.Abstractions;
public sealed record BookingCancelledDomainEvent(Guid bookingId) : IDomainEvent;