namespace Bookify.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    void Add(User user);
}