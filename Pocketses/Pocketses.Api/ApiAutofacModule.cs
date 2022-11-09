using Autofac;
using Pocketses.Core;

namespace Pocketses.Api
{
    public class ApiAutofacModule : CoreAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterServices(typeof(ApiAutofacModule).Assembly, builder);
        }
    }
}
