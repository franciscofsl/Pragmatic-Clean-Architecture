using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bookify.Infrastructure;

// So EF Core can instantiate a DbContext when adding a migration, since it depends on MediatR types.
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer("Server=bookify-db;Database=bookify;User Id=sa;Password=bookify;TrustServerCertificate=True");

        return new ApplicationDbContext(optionsBuilder.Options, publisher: null!);
    }
}