using System.Collections.Immutable;

#pragma warning disable SA1402

namespace Rubrum.Authorization.Relations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RelationAttribute<T1>(string name) : Attribute, IRelationAttribute
    where T1 : DefinitionReference, new()
{
    public string Name => name;

    public ImmutableArray<DefinitionReference> Definitions { get; } = [new T1()];
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RelationAttribute<T1, T2>(string name) : Attribute, IRelationAttribute
    where T1 : DefinitionReference, new()
    where T2 : DefinitionReference, new()
{
    public string Name => name;

    public ImmutableArray<DefinitionReference> Definitions { get; } = [new T1(), new T2()];
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RelationAttribute<T1, T2, T3>(string name) : Attribute, IRelationAttribute
    where T1 : DefinitionReference, new()
    where T2 : DefinitionReference, new()
    where T3 : DefinitionReference, new()
{
    public string Name => name;

    public ImmutableArray<DefinitionReference> Definitions { get; } = [new T1(), new T2(), new T3()];
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RelationAttribute<T1, T2, T3, T4>(string name) : Attribute, IRelationAttribute
    where T1 : DefinitionReference, new()
    where T2 : DefinitionReference, new()
    where T3 : DefinitionReference, new()
    where T4 : DefinitionReference, new()
{
    public string Name => name;

    public ImmutableArray<DefinitionReference> Definitions { get; } = [new T1(), new T2(), new T3(), new T4()];
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RelationAttribute<T1, T2, T3, T4, T5>(string name) : Attribute, IRelationAttribute
    where T1 : DefinitionReference, new()
    where T2 : DefinitionReference, new()
    where T3 : DefinitionReference, new()
    where T4 : DefinitionReference, new()
    where T5 : DefinitionReference, new()
{
    public string Name => name;

    public ImmutableArray<DefinitionReference> Definitions { get; } =
        [new T1(), new T2(), new T3(), new T4(), new T5()];
}
