﻿using System.Collections.Immutable;
using Rubrum.Authorization.Relations;
using Rubrum.Graphql.Relations;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Graphql.SpiceDb.Relations;

[Dependency(ReplaceServices = true)]
public sealed class RelationDirectiveBuilder : IRelationDirectiveBuilder, ITransientDependency
{
    public RelationDirective Build(Relation relation)
    {
        return new RelationDirective(
            Name: relation.Name.ToSnakeCase(),
            Value: string.Join(" | ", CreateRelations(relation)));
    }

    private static ImmutableArray<string> CreateRelations(Relation relation)
    {
        var builder = ImmutableArray.CreateBuilder<string>();

        foreach (var definition in relation.Definitions) // TODO: Реализовать ссылки на relations
        {
            builder.Add(definition.Name.Replace("Definition", string.Empty).ToSnakeCase());
        }

        return builder.ToImmutable();
    }
}
