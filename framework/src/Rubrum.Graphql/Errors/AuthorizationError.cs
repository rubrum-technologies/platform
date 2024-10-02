using Volo.Abp.Authorization;

namespace Rubrum.Graphql.Errors;

public class AuthorizationError(AbpAuthorizationException exception)
{
    public string? Code { get; } = exception.Code;

    public string Message { get; } = exception.Message.ReplaceNewLine() ?? string.Empty;
}
