using Autofac;
using Pocketses.Core.Attributes;
using System.Reflection;
using Module = Autofac.Module;

namespace Pocketses.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterServices(typeof(CoreAutofacModule).Assembly, builder);
        }

        protected void RegisterServices(Assembly assembly, ContainerBuilder builder)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                foreach (var attribute in type.CustomAttributes)
                {
                    if (attribute.AttributeType == typeof(SingletonDependencyAttribute))
                        builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();

                    if (attribute.AttributeType == typeof(TransientDependencyAttribute))
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();

                    if (attribute.AttributeType == typeof(ScopedDependencyAttribute))
                        builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
                }
            }
        }
    }
}
