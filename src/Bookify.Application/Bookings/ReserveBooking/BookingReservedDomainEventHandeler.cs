using MediatR;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;
using Bookify.Application.Abstractions.Email;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler :  INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepositoty;
    private readonly IEmailService _emailService;

    public BookingReservedDomainEventHandler(
        IBookingRepository bookingRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _bookingRepository = bookingRepository;
        _userRepositoty = userRepository;
        _emailService = emailService;
    }
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _bookingRepository.GetByIdAsync(notification.bookingId, cancellationToken);

        if(user is null)
        {
            return;
        }

        await _emailService.SendAsync(
            user.Email,
            "Booking reserved",
            "You have 10 minutes to confirm this booking");
    }
}