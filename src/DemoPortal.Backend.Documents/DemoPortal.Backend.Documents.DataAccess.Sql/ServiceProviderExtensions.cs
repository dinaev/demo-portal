using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DemoPortal.Backend.Documents.DataAccess.Sql;

public static class ServiceProviderExtensions
{
    public static IServiceProvider MigrateDatabaseToLatestVersion(this IServiceProvider serviceProvider)
    {
        if (serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DocumentsContext>();
        dbContext.Database.Migrate();
        return serviceProvider;
    }
}