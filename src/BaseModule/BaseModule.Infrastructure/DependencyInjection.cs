using CoreModule.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BaseModule.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, MachineDateTime>();
        return services;
    }
}
