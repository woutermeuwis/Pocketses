using Autofac;
using AutoMapper;
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

			var assembly = typeof(CoreAutofacModule).Assembly;
			RegisterServices(assembly, builder);
			ConfigureAutoMapper(assembly, builder);
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

		protected void ConfigureAutoMapper(Assembly assembly, ContainerBuilder builder)
		{
			var profiles = assembly.DefinedTypes.Where(t => typeof(Profile).IsAssignableFrom(t)).Select(t => Activator.CreateInstance(t) as Profile).ToArray();
			var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));

#if DEBUG
			configuration.AssertConfigurationIsValid();
#endif

			builder
				.RegisterInstance(configuration.CreateMapper())
				.As<IMapper>()
				.SingleInstance();

		}
	}
}
