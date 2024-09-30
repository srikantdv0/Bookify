namespace Bookify.Domain.Bookings;
using Bookify.Domain.Apartments;
using Bookify.Domain.Users;

public interface IBookingRepository
{
    Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);
     void Add(Booking booking);

     Task<Booking?> GetByIdAsync(Guid bookingId, CancellationToken cancellationToken);
}