using SampleApp.Migrations;
using SampleApp.Repositories;

namespace SampleApp.Extensions;

public static class MigrationExtensions
{
    public static async Task RunMigrationsAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationRunner>>();
            var connectionProvider = scope.ServiceProvider.GetRequiredService<ICockroachDbConnectionProvider>();

            var migrationsDirectory = Path.Combine(AppContext.BaseDirectory, "Migrations");

            var runner = new MigrationRunner(connectionProvider.GetConnectionString(), migrationsDirectory, logger);
            await runner.RunMigrationsAsync();
        }
    }
}
