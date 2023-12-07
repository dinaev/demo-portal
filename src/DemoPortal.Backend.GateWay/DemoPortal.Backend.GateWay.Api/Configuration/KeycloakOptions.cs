namespace DemoPortal.Backend.GateWay.Api.Configuration;

public class KeycloakOptions
{
    public const string SectionName = "Keycloak";
    public string Authority { get; set; } = string.Empty;
    
    public string AuthorizationUrl { get; set; } = string.Empty;
    public string MetadataAddress { get; set; } = string.Empty;
    public string TokenUrl { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Issuer { get; set; }
    public string IssuerContainer { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string SwaggerRedirectUrl { get; set; } = string.Empty;
}