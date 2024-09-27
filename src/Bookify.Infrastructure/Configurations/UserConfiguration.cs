using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.FirstName)
                .HasMaxLength(200)
                .HasConversion(first => first.Value, value => new FirstName(value));
        builder.Property(user => user.LastName)
                .HasMaxLength(200)
                .HasConversion(lastName => lastName.Value,value => new LastName(value));
        builder.Property(user => user.Email)
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Domain.Users.Email(value));
        builder.HasIndex(user => user.Email).IsUnique();

    }
}