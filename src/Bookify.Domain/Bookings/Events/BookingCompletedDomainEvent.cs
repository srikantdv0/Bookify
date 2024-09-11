namespace Bookify.Domain.Bookings.Events;
using Bookify.Domain.Abstractions;
public sealed record BookingCompletedDomainEvent(Guid bookingId) : IDomainEvent;