using System.Text;
using Volo.Abp.Validation;

namespace Rubrum.Graphql.Errors;

public class ValidationError
{
    public ValidationError(AbpValidationException exception)
    {
        Message = GetMessage(exception).ReplaceNewLine() ?? string.Empty;
        Results = exception.ValidationErrors.Select(x => new ValidationResult(x)).ToList();
    }

    public string Message { get; }

    public IReadOnlyList<ValidationResult> Results { get; }

    private static string GetMessage(AbpValidationException exception)
    {
        var validationErrors = exception.ValidationErrors;

        if (validationErrors.IsNullOrEmpty())
        {
            return string.Empty;
        }

        var text = new StringBuilder();

        text.AppendLine("There are " + validationErrors.Count + " validation errors:");

        foreach (var validationResult in validationErrors)
        {
            var memberNames = string.Empty;

            if (validationResult.MemberNames.Any())
            {
                memberNames = $" ({string.Join(", ", validationResult.MemberNames)})";
            }

            text.AppendLine(validationResult.ErrorMessage + memberNames);
        }

        return text.ToString();
    }
}
