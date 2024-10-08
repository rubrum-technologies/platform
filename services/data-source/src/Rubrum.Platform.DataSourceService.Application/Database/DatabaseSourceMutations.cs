﻿using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Types;
using MediatR;
using Rubrum.Graphql.Errors;
using Rubrum.Graphql.Middlewares;
using Rubrum.Platform.DataSourceService.Database.Commands;
using Rubrum.Platform.DataSourceService.Database.Exceptions;
using Rubrum.Platform.DataSourceService.Permissions;

namespace Rubrum.Platform.DataSourceService.Database;

[MutationType]
public static class DatabaseSourceMutations
{
    [Authorize(Policy = DataSourceServicePermissions.DataSources.Create)]
    [UseUnitOfWork]
    [UseAbpError]
    [Error<IncorrectConnectionStringException>]
    [Error<FailConnectException>]
    [Error<DataSourceNameAlreadyExistsException>]
    [Error<DataSourcePrefixAlreadyExistsException>]
    [Error<DatabaseSourceTablesEmptyException>]
    [Error<DatabaseTableNameAlreadyExistsException>]
    [Error<DatabaseTableSystemNameAlreadyExistsException>]
    [Error<DatabaseTableColumnsEmptyException>]
    [Error<DatabaseColumnNameAlreadyExistsException>]
    [Error<DatabaseColumnSystemNameAlreadyExistsException>]
    public static Task<DatabaseSource> CreateDatabaseSourceAsync(
        CreateDatabaseSourceCommand input,
        [Service] IMediator mediator,
        CancellationToken ct = default)
    {
        return mediator.Send(input, ct);
    }
}
