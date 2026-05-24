using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shoootz.Data;

namespace Shoootz.Services.Database;

internal class DatabaseService(IDbContextFactory<AppDbContext> contextFactory) : IDatabaseService
{
    public async Task InitializeAsync()
    {
        using AppDbContext context = await contextFactory.CreateDbContextAsync().ConfigureAwait(false);
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }
}
