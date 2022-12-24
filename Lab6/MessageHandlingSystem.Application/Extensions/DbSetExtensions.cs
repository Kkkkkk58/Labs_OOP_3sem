using Microsoft.EntityFrameworkCore;

namespace MessageHandlingSystem.Application.Extensions;

public static class DbSetExtensions
{
    public static async Task<T> GetEntityAsync<T>(this DbSet<T> set, Guid id, CancellationToken cancellationToken)
        where T : class
    {
        return await set.FindAsync(new object[] { id }, cancellationToken) ?? throw new NotImplementedException();
    }
}