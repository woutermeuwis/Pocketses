using Autofac;
using Pocketses.Core;

namespace Pocketses.Api
{
	public class ApiAutofacModule : CoreAutofacModule
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			var ass = typeof(ApiAutofacModule).Assembly;
			RegisterServices(ass, builder);
			ConfigureAutoMapper(ass, builder);
		}
	}
}
