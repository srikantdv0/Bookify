using Bookify.Domain.Shared;
using Bookify.Domain.Apartments;
namespace Bookify.Domain.Bookings;

public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {   
        var currency  = apartment.Price.Currency;
        var priceForPeriod = new Money(
                  apartment.Price.Amount * period.LengthInDays,
                  currency
                );
        decimal percentUpCharge = 0;
        foreach(var amentity in apartment.Amenities)
        {
            percentUpCharge += amentity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirConditioning => 0.01m,
                Amenity.Prarking => 0.01m,
                _ => 0
            };
        }

        var amenitiesUpCharge = Money.Zero(currency);
        if(percentUpCharge > 0)
        {
            amenitiesUpCharge = new Money(
                priceForPeriod.Amount * percentUpCharge,
                currency
            );
        }
        var totalPrice = Money.Zero();
        totalPrice += priceForPeriod;
        if(!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;
        }
        totalPrice += amenitiesUpCharge;

        return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);
    }
}