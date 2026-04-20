using System;
using Npgsql;

namespace SampleApp.Repositories;

public interface ICockroachDbConnectionProvider
{
    string GetConnectionString();
}

public class CockroachDbConnectionProvider : ICockroachDbConnectionProvider
{
    private readonly IConfiguration _configuration;
    private readonly Lazy<string> _connectionString;

    public CockroachDbConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = new Lazy<string>(BuildConnectionString);
    }

    public string GetConnectionString() => _connectionString.Value;

    private string BuildConnectionString()
    {
        var connStringBuilder = new NpgsqlConnectionStringBuilder
        {
            SslMode = SslMode.VerifyFull
        };

        string databaseUrlEnv = "meh";
        if (databaseUrlEnv == null)
        {
            connStringBuilder.Host = "localhost";
            connStringBuilder.Port = 26257;
            connStringBuilder.Username = "{username}";
            connStringBuilder.Password = "{password}";
        }
        else
        {
            connStringBuilder.Host = _configuration.GetValue<string>("DB_SERVER");
            connStringBuilder.Port = int.TryParse(_configuration.GetValue<string>("DB_PORT"), out int result) ? result : 0;
            connStringBuilder.Username = _configuration.GetValue<string>("DB_USERNAME");
            connStringBuilder.Password = _configuration.GetValue<string>("DB_PASSWORD");
        }
        connStringBuilder.Database = "Billables";

        return connStringBuilder.ConnectionString;
    }
}

// Obsolete - use ICockroachDbConnectionProvider instead
public class CockroachDb(IConfiguration configuration)
{
    public string GetCockroachConnectionString()
    {
        var provider = new CockroachDbConnectionProvider(configuration);
        return provider.GetConnectionString();
    }
}