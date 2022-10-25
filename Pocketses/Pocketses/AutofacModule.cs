using Autofac;
using Autofac.Util;
using Pocketses.Core;
using Pocketses.Core.Attributes;
using System.Linq;

namespace Pocketses.Web
{
    public class WebAutofacModule : CoreAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            RegisterServices(typeof(WebAutofacModule).Assembly, builder);
        }
    }
}
