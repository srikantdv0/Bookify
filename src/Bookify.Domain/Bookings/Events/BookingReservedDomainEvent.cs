namespace Bookify.Domain.Bookings.Events;
using Bookify.Domain.Abstractions;
public sealed record BookingReservedDomainEvent(Guid bookingId) : IDomainEvent;