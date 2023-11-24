using DemoPortal.Backend.Documents.Api.Client;
using DemoPortal.Backend.GateWay.Web;
using DemoPortal.Backend.GateWay.Web.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add authentication.
builder.Services.AddDemoPortalAuthentication(builder.Configuration);

// Add automapper profiles.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Add lowercase URLs.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// ADD documents API client.
builder.Services.AddDocumentsApiClient(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

// Add swagger.
builder.Services.AddDemoPortalSwaggerGen(builder.Configuration);

var app = builder.Build();
var keycloakOptions = new KeycloakOptions();
builder.Configuration.GetSection(KeycloakOptions.SectionName).Bind(keycloakOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Portal API v1");
        options.DocExpansion(DocExpansion.List);
        options.EnableDeepLinking();

        // Enable OAuth2 authorization support in Swagger UI
        options.OAuthClientId(keycloakOptions.ClientId);
        options.OAuthClientSecret(keycloakOptions.ClientSecret);
        options.OAuthAppName("Swagger");
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        options.OAuth2RedirectUrl(keycloakOptions.SwaggerRedirectUrl);
        options.OAuthUsePkce();
        options.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();