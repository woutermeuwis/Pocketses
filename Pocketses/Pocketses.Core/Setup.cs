using Pocketses.Core.Models;

namespace Pocketses.Core;

public static class Setup
{
    public static IServiceProvider InitializeCore(this IServiceProvider serviceProvider)
    {
        SeedData.Initialize(serviceProvider);
        return serviceProvider;
    }
}
