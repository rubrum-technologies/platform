namespace MyCompanyName.MyProjectName;

public static class MyProjectNameDbProperties
{
    public const string ConnectionStringName = "MyProjectName";

    public static string DbTablePrefix { get; set; } = "MyCompanyName";

    public static string? DbSchema { get; set; } = null;
}
