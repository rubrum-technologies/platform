using FluentValidation;

namespace Rubrum.Platform.WindowsService.Windows.Commands.Validators;

public class WindowSizeValidator : AbstractValidator<WindowSize>
{
    public WindowSizeValidator()
    {
        RuleFor(x => x.Width)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Height)
            .GreaterThanOrEqualTo(0);
    }
}
