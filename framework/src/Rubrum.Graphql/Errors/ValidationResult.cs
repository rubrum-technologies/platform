namespace Rubrum.Graphql.Errors;

public class ValidationResult
{
    public ValidationResult(System.ComponentModel.DataAnnotations.ValidationResult result)
    {
        ErrorMessage = result.ErrorMessage;
    }

    public string? ErrorMessage { get; }
}
