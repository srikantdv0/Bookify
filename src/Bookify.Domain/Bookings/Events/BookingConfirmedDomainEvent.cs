namespace Bookify.Domain.Bookings.Events;
using Bookify.Domain.Abstractions;
public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;