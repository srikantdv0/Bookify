namespace Bookify.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIDAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(User user);
}