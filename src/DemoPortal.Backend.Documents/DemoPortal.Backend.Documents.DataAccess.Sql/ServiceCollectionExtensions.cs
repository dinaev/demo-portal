using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Documents.DataAccess.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoPortal.Backend.Documents.DataAccess.Sql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocumentsDataAccessSql(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DocumentsContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DemoPortalDocuments")));
        
        services.AddScoped<IDocumentsRepository, DocumentsRepository>();
        
        return services;
    }
}