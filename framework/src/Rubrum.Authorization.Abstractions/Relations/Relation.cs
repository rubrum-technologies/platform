﻿using System.Collections.Immutable;

#pragma warning disable S4050

namespace Rubrum.Authorization.Relations;

public abstract class Relation(string name, params DefinitionReference[] definitions)
{
    public string Name => name;

    public ImmutableArray<DefinitionReference> Definitions { get; } = [..definitions];

    public static implicit operator Permission(Relation relation) => relation.ToPermission();

    public static Permission operator +(Relation a, RelationProperty b) => a.ToPermission() + b;

    public static Permission operator +(RelationProperty a, Relation b) => a + b.ToPermission();

    public static Permission operator +(Relation a, Relation b) => a.ToPermission() + b.ToPermission();

    public static Permission operator -(RelationProperty a, Relation b) => a - b.ToPermission();

    public static Permission operator -(Relation a, RelationProperty b) => a.ToPermission() - b;

    public static Permission operator -(Relation a, Relation b) => a.ToPermission() - b.ToPermission();

    public static Permission operator &(RelationProperty a, Relation b) => a & b.ToPermission();

    public static Permission operator &(Relation a, RelationProperty b) => a.ToPermission() & b;

    public static Permission operator &(Relation a, Relation b) => a.ToPermission() & b.ToPermission();

    private PermissionField ToPermission()
    {
        return new PermissionField(Name);
    }
}
