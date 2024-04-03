namespace Cinema;

public class ExportSqlOptions
{
    public string PostgresSqlFolder { get; init; } = null!;

    public string ConnectionString { get; init; } = null!;

    public string BackupBatName { get; init; } = null!;

    public string RestoreBatName { get; init; } = null!;

    public string RestoreFile { get; init; } = null!;

    public string ResultFile { get; init; } = null!;

    public string GetConnectionUri()
    {
        var connectionOptions = ConnectionString.Split(';')
            .Select(pair => pair.Split('=', 2))
            .ToDictionary(pair => pair[0].Trim(), pair => pair[1].Trim(), StringComparer.InvariantCultureIgnoreCase);
        var host = connectionOptions["Host"];
        var port = connectionOptions["Port"];
        var database = connectionOptions["Database"];
        var user = connectionOptions["Username"];
        var password = connectionOptions["Password"];
        return $"postgres://{user}:{password}@{host}:{port}/{database}";
    }
}