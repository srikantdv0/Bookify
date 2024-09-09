namespace Bookify.Domain.Bookings;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;

public sealed class Booking : Entity
{
    private Booking(
        Guid id,
        Guid apartmentId,
        Guid userId,
        DateRange duration,
        Money priceForPeriod,
        Money cleaningFee,
        Money amenitiesUpCharge,
        Money totalPrice,
        BookingStatus status,
        DateTime createOnUtc
        ) : base(id)
    {
        ApartmentId = apartmentId;
        UserId = userId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        TotalPrice = totalPrice;
        Status = status;
        CreateOnUtc = createOnUtc;
    }

    public Guid ApartmentId {get; private set;}
    public Guid UserId {get; private set;}
    public DateRange Duration {get; private set;}
    public Money PriceForPeriod {get; private set;}
    public Money CleaningFee {get; private set;}
    public Money AmenitiesUpCharge {get; private set;}
    public Money TotalPrice {get; private set;}
    public BookingStatus Status {get; private set;}
    public DateTime CreateOnUtc {get; private set;}
    public DateTime? ConfirmedOnUtc {get; private set;}
    public DateTime? RejecttedOnUtc {get; private set;}
    public DateTime? CompletedOnUtc {get; private set;}
    public DateTime? CancelledOnUtc {get; private set;}


    public static Booking Reserve(
        Guid apartmentId,
        Guid userId,
        DateRange duration,
        DateTime utcNow)
    {
        var booking = new Booking(
            Guid.NewGuid(),
            apartmentId,
            userId,
            duration);
        return booking;

    }

}