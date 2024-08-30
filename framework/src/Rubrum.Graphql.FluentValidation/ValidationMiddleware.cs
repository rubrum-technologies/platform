using System.ComponentModel.DataAnnotations;
using FluentValidation;
using HotChocolate.Resolvers;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Validation;

namespace Rubrum.Graphql;

internal class ValidationMiddleware(FieldDelegate next)
{
    public const string MiddlewareIdentifier = "Iga.Abp.Graphql.FluentValidation.ValidationMiddleware";

    public async Task InvokeAsync(IMiddlewareContext context)
    {
        var arguments = context.Selection.Field.Arguments;

        foreach (var argument in arguments)
        {
            var value = context.ArgumentValue<object?>(argument.Name);
            if (value == null)
            {
                continue;
            }

            if (!argument.ContextData.TryGetValue(WellKnownContextData.Validator, out var validatorType))
            {
                continue;
            }

            var validator = (IValidator)context.Services.GetRequiredService((Type)validatorType!);
            var validationContext = new ValidationContext<object>(value);
            var validationResult = await validator.ValidateAsync(
                validationContext,
                context.RequestAborted);

            if (validationResult is { IsValid: false })
            {
                throw new AbpValidationException(validationResult.Errors
                    .Select(x => new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }))
                    .ToList());
            }
        }

        await next(context).ConfigureAwait(false);
    }
}
