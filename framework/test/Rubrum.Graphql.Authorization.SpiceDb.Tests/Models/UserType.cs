﻿using HotChocolate.Types;
using Rubrum.Graphql;

namespace Rubrum.Authorization.Analyzers.Models;

[ObjectType<User>]
public static partial class UserType
{
    static partial void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Entity();
    }
}
