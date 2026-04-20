using Npgsql;

namespace SampleApp.Migrations;

public class MigrationRunner
{
    private readonly string _connectionString;
    private readonly string _migrationsDirectory;
    private readonly ILogger<MigrationRunner> _logger;
    private const string MigrationsTable = "schema_migrations";

    public MigrationRunner(string connectionString, string migrationsDirectory, ILogger<MigrationRunner> logger)
    {
        _connectionString = connectionString;
        _migrationsDirectory = migrationsDirectory;
        _logger = logger;
    }

    public async Task RunMigrationsAsync()
    {
        try
        {
            await CreateMigrationsTableAsync();
            var migrationFiles = GetMigrationFiles();

            if (migrationFiles.Count == 0)
            {
                _logger.LogInformation("No migration files found in {MigrationsDirectory}", _migrationsDirectory);
                return;
            }

            // Get already applied migrations
            var appliedMigrations = await GetAppliedMigrationsAsync();

            // Execute pending migrations in order
            foreach (var migration in migrationFiles)
            {
                if (!appliedMigrations.Contains(migration.Version))
                {
                    await ExecuteMigrationAsync(migration);
                }
            }

            _logger.LogInformation("Migrations completed successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error running migrations");
            throw;
        }
    }

    private List<Migration> GetMigrationFiles()
    {
        var migrations = new List<Migration>();

        if (!Directory.Exists(_migrationsDirectory))
        {
            _logger.LogWarning("Migrations directory not found: {MigrationsDirectory}", _migrationsDirectory);
            return migrations;
        }

        var migrationFiles = Directory.GetFiles(_migrationsDirectory, "*.sql")
            .OrderBy(f => f)
            .ToList();

        foreach (var file in migrationFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            // Expected format: ###_description.sql
            var parts = fileName.Split('_', 2);

            if (parts.Length == 2 && int.TryParse(parts[0], out _))
            {
                migrations.Add(new Migration
                {
                    Version = parts[0],
                    Description = parts[1],
                    FilePath = file
                });
            }
        }

        return migrations;
    }

    private async Task CreateMigrationsTableAsync()
    {
        _logger.LogDebug("Ensuring migrations table exists...");
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $@"
                    CREATE TABLE IF NOT EXISTS {MigrationsTable} (
                        version VARCHAR(255) NOT NULL PRIMARY KEY,
                        description VARCHAR(255),
                        installed_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                    );";

                await cmd.ExecuteNonQueryAsync();
            }
        }

        _logger.LogDebug("Migrations table ensured");
    }

    private async Task<HashSet<string>> GetAppliedMigrationsAsync()
    {
        var applied = new HashSet<string>();

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT version FROM {MigrationsTable};";

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        applied.Add(reader.GetString(0));
                    }
                }
            }
        }

        return applied;
    }

    private async Task ExecuteMigrationAsync(Migration migration)
    {
        try
        {
            var sql = File.ReadAllText(migration.FilePath);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Execute the migration SQL
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = sql;
                    await cmd.ExecuteNonQueryAsync();
                }

                // Record the migration
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $@"
                        INSERT INTO {MigrationsTable} (version, description)
                        VALUES (@version, @description);";

                    cmd.Parameters.AddWithValue("@version", migration.Version);
                    cmd.Parameters.AddWithValue("@description", migration.Description);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            _logger.LogInformation("Migration {Version} ({Description}) applied successfully", migration.Version, migration.Description);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing migration {Version}", migration.Version);
            throw;
        }
    }
}
