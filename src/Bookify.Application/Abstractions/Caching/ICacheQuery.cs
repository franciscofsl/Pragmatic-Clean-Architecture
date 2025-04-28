namespace Bookify.Application.Abstractions.Caching;

public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}