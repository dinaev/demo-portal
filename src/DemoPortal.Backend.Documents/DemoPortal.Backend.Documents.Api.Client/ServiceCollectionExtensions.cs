using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace DemoPortal.Backend.Documents.Api.Client;

/// <summary>
/// Service collection extension to add documents API client
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add documents API client
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDocumentsApiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = GetRefitSettings();
            
        services.Configure<DocumentsApiConfiguration>(
            configuration.GetSection(DocumentsApiConfiguration.ConfigSectionName)
        );
            
        services.AddRefitClient<IDocumentsApi>(settings)
            .ConfigureHttpClient((serviceProvider, httpClient) => 
                httpClient.BaseAddress = new Uri(GetApiUrl(serviceProvider))
            );
        
        return services;
    }
    
    private static string GetApiUrl(IServiceProvider serviceProvider)
    {
        var config = serviceProvider.GetService<IOptions<DocumentsApiConfiguration>>()?.Value;
        if (config == null)
            throw new ArgumentNullException($"Configuration section '{DocumentsApiConfiguration.ConfigSectionName}' is required");

        if (string.IsNullOrEmpty(config.BaseUrl))
            throw new ArgumentNullException($"Configuration parameter '{nameof(config.BaseUrl)}' in section '{DocumentsApiConfiguration.ConfigSectionName}' is required");

        return config.BaseUrl;
    }
        
    private static RefitSettings GetRefitSettings()
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.PropertyNameCaseInsensitive = true;
        jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        jsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());
            
        return new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
        };
    }
}