using Microsoft.CodeAnalysis;

namespace Rubrum.Authorization.Analyzers.Models;

public class PermissionInfo
{
    public PermissionInfo(AttributeData attributeData)
    {
        PropertyName = attributeData
            .ConstructorArguments[0]
            .Value!
            .ToString()
            .Trim('"');

        AttributeData = attributeData;
    }

    public string PropertyName { get; }

    public AttributeData AttributeData { get; }
}
