using Volo.Abp;

namespace Rubrum.Graphql.Errors;

public class BusinessError(BusinessException exception)
{
    public string? Code { get; } = exception.Code;

    public string? Details { get; } = exception.Details;

    public string Message { get; } = exception.Message.ReplaceNewLine() ?? string.Empty;
}
