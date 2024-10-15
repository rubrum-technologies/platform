using FluentValidation;

namespace Rubrum.Platform.StoreAppsService.Apps.Commands.Validators;

public class AppVersionValidator : AbstractValidator<AppVersion>
{
    public AppVersionValidator()
    {
        RuleFor(x => x.Major)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Minor)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Patch)
            .GreaterThanOrEqualTo(0);
    }
}
