using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Bookify.Application.Abstractions.Clock;
using Bookify.Infrastructure.Clock;
using Bookify.Application.Abstractions.Email;
using Bookify.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructrue(
                    this IServiceCollection services
                    ,IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") ??
                                throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationDbContext>(options => 
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        return services;
    }
}