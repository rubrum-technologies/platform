namespace Rubrum.Platform.DataSourceService.Database.Exceptions;

public class FailConnectException(string message, string connectionString) : Exception(message)
{
    public string ConnectionString => connectionString;
}
