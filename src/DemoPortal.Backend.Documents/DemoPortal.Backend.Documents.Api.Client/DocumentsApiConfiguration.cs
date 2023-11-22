namespace DemoPortal.Backend.Documents.Api.Client;

/// <summary>
/// Document API configuration
/// </summary>
public class DocumentsApiConfiguration
{
    /// <summary>
    /// Config section name
    /// </summary>
    public const string ConfigSectionName = "DocumentsApiSettings";

    /// <summary>
    /// Base URL of documents API
    /// </summary>
    public string BaseUrl { get; set; }
}