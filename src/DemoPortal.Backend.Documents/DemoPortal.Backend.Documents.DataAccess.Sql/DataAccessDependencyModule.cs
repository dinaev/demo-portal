using Autofac;
using DemoPortal.Backend.Documents.Abstractions.Repositories;
using DemoPortal.Backend.Documents.DataAccess.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DemoPortal.Backend.Documents.DataAccess.Sql
{
    public class DataAccessDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => 
                {
                    var config = c.Resolve<IConfiguration>();

                    var optionsBuilder = new DbContextOptionsBuilder<DocumentsContext>();
                    optionsBuilder.UseNpgsql(config.GetConnectionString("DemoPortalDocuments"));

                    return new DocumentsContext(optionsBuilder.Options);
                })
                .AsSelf()
                .InstancePerDependency();
            
            builder.RegisterType<DocumentsRepository>().As<IDocumentsRepository>()
                .InstancePerLifetimeScope();
        }
    }
}