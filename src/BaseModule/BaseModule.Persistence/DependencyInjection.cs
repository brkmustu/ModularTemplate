using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BaseModule.Common;

namespace BaseModule;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        /// persistence layer registrations
        /// 

        string connectionString = configuration.GetDefaultConnectionString();

        services.AddDbContext<BaseModuleDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IBaseModuleDbContext>(provider => provider.GetService<BaseModuleDbContext>());

        return services;
    }
    public static string GetDefaultConnectionString(this IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_BaseModule");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("BaseModule");
        }
        return connectionString;
    }
}
