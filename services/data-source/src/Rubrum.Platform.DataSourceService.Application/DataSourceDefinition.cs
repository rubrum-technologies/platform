using Rubrum.Authorization.Relations;
using Rubrum.Platform.Relations;

namespace Rubrum.Platform.DataSourceService;

[Definition]
[Relation<UserDefinition.Ref, UserDefinition.Ref.All>("Reader")]
[Relation<UserDefinition.Ref>("Owner")]
[Permission("View")]
[Permission("Edit")]
public static partial class DataSourceDefinition
{
    public static partial Permission ViewConfigure() => View + Edit;

    public static partial Permission EditConfigure() => Owner;
}
