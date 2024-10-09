namespace Rubrum.Platform.DataSourceService;

public enum DataSourceRelationDirection
{
    /// <summary>
    /// OneToMany.
    /// </summary>
    OneToMany,

    /// <summary>
    /// ManyToOne.
    /// </summary>
    ManyToOne,

    // TODO(#11): Добавить One-to-One
}
