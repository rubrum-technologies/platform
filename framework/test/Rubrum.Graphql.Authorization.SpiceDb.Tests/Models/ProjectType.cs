﻿using HotChocolate.Types;
using Rubrum.Graphql;
using Rubrum.Graphql.Relations;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<Project>]
public static partial class ProjectType
{
    static partial void Configure(IObjectTypeDescriptor<Project> descriptor)
    {
        descriptor.Entity();

        descriptor.BindDefinition(typeof(ProjectDefinition));
    }
}
