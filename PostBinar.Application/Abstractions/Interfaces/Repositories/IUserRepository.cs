using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Repositories;

public interface IUserRepository
{
    void Add(User user);
    void Delete(User user);

    Task<User?> GetByIdAsync(
        UserId id,
        CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);
}
