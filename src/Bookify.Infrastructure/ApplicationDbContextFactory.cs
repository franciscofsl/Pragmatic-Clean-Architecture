using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookify.Infrastructure;

// So EF Core can instantiate a DbContext when adding a migration, since it depends on MediatR types.
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseNpgsql("Host=bookify-db;Port=5432;Database=bookify;Username=postgres;Password=postgres;");

        return new ApplicationDbContext(optionsBuilder.Options, publisher: null!);
    }
}