using Microsoft.EntityFrameworkCore;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookify.Domain.Shared;
using Bookify.Domain.Apartments;
using Bookify.Domain.Users;
namespace Bookify.Infrastructure.Configurations;

internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("booking");
        builder.HasKey(booking => booking.Id);
        builder.OwnsOne(booking => booking.PriceForPeriod, priceBuilder =>
                        {
                            priceBuilder.Property(money => money.Currency)
                                    .HasConversion(currency => currency.Code, code => Currency.FormCode(code));
                        });
        builder.OwnsOne(booking => booking.CleaningFee, priceBuilder =>
                        {
                            priceBuilder.Property(money => money.Currency)
                                        .HasConversion(currency => currency.Code, code => Currency.FormCode(code));
                        });
        builder.OwnsOne(booking => booking.AmenitiesUpCharge, priceBuilder =>
                        {
                            priceBuilder.Property(money => money.Currency)
                                        .HasConversion(currency => currency.Code, code => Currency.FormCode(code));
                        });
        builder.OwnsOne(booking => booking.TotalPrice, priceBuilder =>
                        {
                            priceBuilder.Property(money => money.Currency)
                                        .HasConversion(currency => currency.Code, code => Currency.FormCode(code));
                        });
        builder.OwnsOne(booking => booking.Duration);
        builder.HasOne<Apartment>()
                    .WithMany()
                    .HasForeignKey(booking => booking.ApartmentId); //Apartment could have many bookings
        builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(booking => booking.UserId); //user could have many bookings
}
    
}