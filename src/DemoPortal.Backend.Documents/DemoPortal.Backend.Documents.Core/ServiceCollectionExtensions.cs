using DemoPortal.Backend.Documents.Abstractions.Services;
using DemoPortal.Backend.Documents.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DemoPortal.Backend.Documents.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocumentsCore(
        this IServiceCollection services)
    {
        services.AddScoped<IDocumentsService, DocumentsService>();
        
        return services;
    }
}