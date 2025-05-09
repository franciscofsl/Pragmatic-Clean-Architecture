﻿namespace Bookify.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    void Add(User entity);
}