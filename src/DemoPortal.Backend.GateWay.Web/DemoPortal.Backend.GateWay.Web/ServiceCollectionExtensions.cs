using System.Reflection;
using DemoPortal.Backend.GateWay.Web.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace DemoPortal.Backend.GateWay.Web;

/// <summary>
/// Service collection extensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add authentication methods
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddDemoPortalAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var keycloakOptions = new KeycloakOptions();
        configuration.GetSection(KeycloakOptions.SectionName).Bind(keycloakOptions);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.MetadataAddress = keycloakOptions.MetadataAddress;
                options.Authority = keycloakOptions.Authority;
                options.Audience = keycloakOptions.Audience;
                options.RequireHttpsMetadata = keycloakOptions.RequireHttpsMetadata;
            })
            .AddOpenIdConnect(options =>
            {
                options.Authority = keycloakOptions.Authority;
                options.ClientId = keycloakOptions.ClientId;
                options.ClientSecret = keycloakOptions.ClientSecret;
        
                // "code" refers to the Authorization Code
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.RequireHttpsMetadata = keycloakOptions.RequireHttpsMetadata;
            });
        return services;
    }

    /// <summary>
    /// Add authentication methods
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <returns></returns>
    public static IServiceCollection AddDemoPortalSwaggerGen(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var keycloakOptions = new KeycloakOptions();
        configuration.GetSection(KeycloakOptions.SectionName).Bind(keycloakOptions);
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            var bearerScheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer {token}' here",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            var oauthScheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(keycloakOptions.AuthorizationUrl),
                        TokenUrl = new Uri(keycloakOptions.TokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            {"openid", "Open ID"},
                            {"profile", "Profile"}
                        }
                    }
                },
                Type = SecuritySchemeType.OAuth2
            };

            options.AddSecurityDefinition("Bearer", bearerScheme);
            options.AddSecurityDefinition("OAuth", oauthScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "OAuth",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
}