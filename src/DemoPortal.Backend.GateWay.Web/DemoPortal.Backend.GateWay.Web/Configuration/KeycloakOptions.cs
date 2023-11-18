﻿namespace DemoPortal.Backend.GateWay.Web.Configuration;

public class KeycloakOptions
{
    public const string SectionName = "Keycloak";
    public string Authority { get; set; } = string.Empty;
    public string MetadataAddress { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; }
}