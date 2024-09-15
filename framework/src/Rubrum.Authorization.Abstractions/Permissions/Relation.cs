using System.Collections.Immutable;

#pragma warning disable S4050

namespace Rubrum.Authorization.Permissions;

public abstract class Relation<T>(string name, params Type[] definitions) : IRelation
    where T : Relation<T>
{
    public string Name => name;

    public ImmutableArray<Type> Definitions { get; } = [..definitions];

    public static implicit operator Permission(Relation<T> relation) => new();

    public static Permission operator +(Permission a, Relation<T> b) => new();

    public static Permission operator +(Relation<T> a, Permission b) => new();

    public static Permission operator +(Relation<T> a, Relation<T> b) => new();

    public static Permission operator -(Permission a, Relation<T> b) => new();

    public static Permission operator -(Relation<T> a, Permission b) => new();

    public static Permission operator -(Relation<T> a, Relation<T> b) => new();

    public static Permission operator &(Permission a, Relation<T> b) => new();

    public static Permission operator &(Relation<T> a, Permission b) => new();

    public static Permission operator &(Relation<T> a, Relation<T> b) => new();
}
