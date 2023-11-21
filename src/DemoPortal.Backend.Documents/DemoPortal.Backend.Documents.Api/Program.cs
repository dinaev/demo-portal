using Autofac;
using Autofac.Extensions.DependencyInjection;
using DemoPortal.Backend.Documents.Api;
using DemoPortal.Backend.Documents.Core;
using DemoPortal.Backend.Documents.DataAccess.Sql;

var builder = WebApplication.CreateBuilder(args);

// Register dependency modules.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
    containerBuilder =>
    {
        containerBuilder.RegisterModule(new MapperDependencyModule());
        containerBuilder.RegisterModule(new CoreDependencyModule());
        containerBuilder.RegisterModule(new DataAccessDependencyModule());
    });

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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