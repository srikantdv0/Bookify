namespace Bookify.Domain.Bookings.Events;
using Bookify.Domain.Abstractions;
public sealed record BookingRejectedDomainEvent(Guid bookingId) : IDomainEvent;