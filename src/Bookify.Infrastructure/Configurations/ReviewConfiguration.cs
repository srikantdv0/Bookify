using Bookify.Domain.Reviews;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("review");
        builder.HasKey(review => review.Id);
        builder.HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(review => review.ApartmentId);
        builder.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(review => review.BookingId);
        builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(review => review.UserId);
        builder.OwnsOne(review => review.Rating);
        builder.OwnsOne(review => review.Comment);
        
    }
}