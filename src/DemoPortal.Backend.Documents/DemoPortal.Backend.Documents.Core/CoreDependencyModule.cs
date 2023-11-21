using Autofac;
using DemoPortal.Backend.Documents.Abstractions.Services;
using DemoPortal.Backend.Documents.Core.Services;

namespace DemoPortal.Backend.Documents.Core
{
    public class CoreDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentsService>().As<IDocumentsService>().InstancePerLifetimeScope();
        }
    }
}
