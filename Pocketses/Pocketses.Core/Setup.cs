using Microsoft.Extensions.DependencyInjection;
using Pocketses.Core.AppServices;
using Pocketses.Core.AppServices.Interfaces;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Models;
using Pocketses.Core.Repositories;
using Pocketses.Core.Repositories.Interfaces;

namespace Pocketses.Core;

public static class Setup
{
    public static IServiceCollection InitializeCore(this IServiceCollection serviceCollection)
    {
        return serviceCollection
                        .AddRepositoryDependencyGroup()
                        .AddServiceDedependencyGroup();
    }

    public static IServiceProvider InitializeCore(this IServiceProvider serviceProvider)
    {
        return serviceProvider.SeedDb();
    }

    private static IServiceCollection AddRepositoryDependencyGroup(this IServiceCollection services)
    {
        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();

        return services;
    }

    private static IServiceCollection AddServiceDedependencyGroup(this IServiceCollection services)
    {
        services.AddScoped<ICampaignAppService, CampaignAppService>();
        services.AddScoped<IPlayerAppService, PlayerAppService>();
        services.AddScoped<ICharacterAppService, CharacterAppService>();

        return services;
    }

    private static IServiceProvider SeedDb(this IServiceProvider serviceProvider)
    {
        SeedData.Initialize(serviceProvider);
        return serviceProvider;
    }
}
