namespace Bookify.Domain.Bookings;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Shared;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Apartments;
using System.Runtime.CompilerServices;
using System.Xml.XPath;

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
    public DateTime? RejectedOnUtc {get; private set;}
    public DateTime? CompletedOnUtc {get; private set;}
    public DateTime? CancelledOnUtc {get; private set;}


    public static Booking Reserve(
        Apartment apartment,
        Guid userId,
        DateRange duration,
        DateTime utcNow,
        PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(apartment, duration);
        var booking = new Booking(
            Guid.NewGuid(),
            apartment.Id,
            userId,
            duration,
            pricingDetails.PricingForPeriod,
            pricingDetails.CleaningFee,
            pricingDetails.AmentiesUpCharge,
            pricingDetails.TotalPrice,
            BookingStatus.Reserved,
            utcNow);
        
        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));
        apartment.LastBookedOnUTC = utcNow;
        return booking;
    }

    public Result Confirm(DateTime utcNow)
    {
        if(Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotPending);
        }
        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));
        return Result.Sucess();
    }

    public Result Reject(DateTime utcNow)
    {
        if(Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotPending);
        }
        Status = BookingStatus.Rejected;
        RejectedOnUtc = utcNow;

        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));
        return Result.Sucess();
    }

    public Result Complete(DateTime utcNow)
    {
        if(Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }
        Status = BookingStatus.Completed;
        RejectedOnUtc = utcNow;

        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));
        return Result.Sucess();
    }

    public Result Cancel(DateTime utcNow)
    {
        if(Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }
        var currentDate  = DateOnly.FromDateTime(utcNow);
        
        if(currentDate > Duration.Start)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }
        Status = BookingStatus.Cancelled;
        CancelledOnUtc = utcNow;

        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));
        return Result.Sucess();
    }

}