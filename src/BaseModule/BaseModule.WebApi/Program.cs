using Microsoft.EntityFrameworkCore;
using BaseModule;
using BaseModule.System.SeedSampleData;
using CoreModule.Web.Swagger;
using Microsoft.OpenApi.Models;
using CoreModule.Web.Codes;
using BaseModule.Infrastructure;
using BaseModule.System.Permissions;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Open http://localhost:5132/swagger/ to browse the API.
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "SOLID Services API" });

    // The XML comment files are copied using a post-build event (see project settings / Build Events).
    options.IncludeXmlDocumentationFromDirectory(AppDomain.CurrentDomain.BaseDirectory);

    // Optional but useful: this includes the summaries of the command and query types in the operations.
    options.IncludeMessageSummariesFromXmlDocs(AppDomain.CurrentDomain.BaseDirectory);
});

services.AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration)
    .AddWebApi(builder.Configuration);

var container = new Container();

services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore();
});

Bootstrapper.Bootstrap(container);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;

    try
    {
        var baseModuleContext = scopedServices.GetRequiredService<BaseModuleDbContext>();
        baseModuleContext.Database.Migrate();

        var sampleDataSeeder = scopedServices.GetRequiredService<SampleDataSeeder>();
        await sampleDataSeeder.SeedAllAsync(CancellationToken.None);

        await new SyncPermissions().SyncAllAsync(baseModuleContext);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

app.MapCommands(
    pattern: MessageMapping.FlatApi(new Commands(container), "/api/commands/{0}"),
    commandTypes: Bootstrapper.GetKnownCommandTypes());
app.MapQueries(
    pattern: MessageMapping.FlatApi(new Queries(container), "/api/queries/{0}"),
    queryTypes: Bootstrapper.GetKnownQueryTypes());

app.Run();
