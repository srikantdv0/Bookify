namespace Bookify.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartments?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}