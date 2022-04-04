using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BaseModule
{
    public class BaseModuleDbContextFactory : IDesignTimeDbContextFactory<BaseModuleDbContext>
    {
        public BaseModuleDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<BaseModuleDbContext>()
                .UseNpgsql(configuration.GetDefaultConnectionString());

            return new BaseModuleDbContext(builder.Options);
        }
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BaseModule.WebApi/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
