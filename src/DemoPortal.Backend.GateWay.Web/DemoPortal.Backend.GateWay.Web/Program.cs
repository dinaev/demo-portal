using System.Reflection;
using DemoPortal.Backend.Documents.Api.Client;
using DemoPortal.Backend.GateWay.Web.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
var keycloakOptions = new KeycloakOptions();
builder.Configuration.GetSection(KeycloakOptions.SectionName).Bind(keycloakOptions);
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

// Add automapper profiles.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Add lowercase URLs.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// ADD documents API client.
builder.Services.AddDocumentsApiClient(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
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
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { } 
        }
    });
});

var app = builder.Build();

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
        options.OAuthAppName("Swagger");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();