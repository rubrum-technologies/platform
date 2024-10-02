namespace Rubrum.Platform.DataSourceService.Database.Exceptions;

public class IncorrectConnectionStringException(string message, string connectionString) : Exception(message)
{
    public string IncorrectConnectionString => connectionString;
}
