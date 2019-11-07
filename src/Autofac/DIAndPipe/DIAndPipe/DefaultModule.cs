using Autofac;
using DIAndPipe.Services.Declare;
using DIAndPipe.Services.Implement;

namespace DIAndPipe
{
    public class DefaultModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().As<IPersonService>().PropertiesAutowired().InstancePerLifetimeScope();
        }
    }
}