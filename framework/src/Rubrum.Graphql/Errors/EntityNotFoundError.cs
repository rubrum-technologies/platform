using Volo.Abp.Domain.Entities;

namespace Rubrum.Graphql.Errors;

public class EntityNotFoundError(EntityNotFoundException exception)
{
    public string? Id { get; } = exception.Id?.ToString();

    public string? Type { get; } = exception.EntityType?.Name;

    public string Message { get; } = exception.Message.ReplaceNewLine() ?? string.Empty;
}
