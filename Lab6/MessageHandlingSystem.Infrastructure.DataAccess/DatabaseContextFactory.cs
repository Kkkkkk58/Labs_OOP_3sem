using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MessageHandlingSystem.Infrastructure.DataAccess;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlite("Data Source=database.db").UseLazyLoadingProxies();

        return new DatabaseContext(optionsBuilder.Options);
    }
}