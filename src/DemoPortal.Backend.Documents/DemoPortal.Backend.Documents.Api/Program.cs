using System.Reflection;
using DemoPortal.Backend.Documents.Core;
using DemoPortal.Backend.Documents.DataAccess.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add automapper profiles.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add data access layer.
builder.Services.AddDocumentsDataAccessSql(builder.Configuration);

// Add business logic.
builder.Services.AddDocumentsCore();

// Add services to the container.
builder.Services.AddControllers();

// Add lowercase URLs.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();