using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cache;

    public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cache = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var cacheKey = $"auth:roles-{identityId}";
        var cachedRoles = await _cache.GetAsync<UserRolesResponse>(cacheKey);
        if (cachedRoles is not null)
        {
            return cachedRoles;
        }

        var users = _dbContext.Set<User>();

        var roles = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .Select(u => new UserRolesResponse
            {
                UserId = u.Id,
                Roles = u.Roles.ToList()
            })
            .FirstOrDefaultAsync();

        await _cache.SetAsync(cacheKey, roles);

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        var cacheKey = $"auth:permissions-{identityId}";
        var cachedPermissions = await _cache.GetAsync<HashSet<string>>(cacheKey);
        if (cachedPermissions is not null)
        {
            return cachedPermissions;
        }
        
        var permissions = await _dbContext.Set<User>()
            .Where(u => u.IdentityId == identityId)
            .SelectMany(u => u.Roles.Select(r => r.Permissions))
            .FirstAsync();
        
        await _cache.SetAsync(cacheKey, permissions);

        return permissions.Select(p => p.Name).ToHashSet();
    }
}