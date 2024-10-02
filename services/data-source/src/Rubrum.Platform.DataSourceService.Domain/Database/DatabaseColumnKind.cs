namespace Rubrum.Platform.DataSourceService.Database;

public enum DatabaseColumnKind
{
    /// <summary>
    /// Boolean.
    /// </summary>
    Boolean,

    /// <summary>
    /// Number.
    /// </summary>
    Number,

    /// <summary>
    /// String.
    /// </summary>
    String,

    /// <summary>
    /// Uuid.
    /// </summary>
    Uuid,

    /// <summary>
    /// DateTime.
    /// </summary>
    DateTime,

    /// <summary>
    /// Unknown.
    /// </summary>
    Unknown,
}
