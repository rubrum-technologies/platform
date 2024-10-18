using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Rubrum.Platform.WindowsService.Windows;

public class WindowManager(IWindowRepository repository) : DomainService
{
    public async Task<Window> CreateAsync(
        Guid ownerId,
        string name,
        Guid appId,
        WindowPosition position,
        WindowSize size,
        CancellationToken ct = default)
    {
        await CheckNameAsync(name, ct);

        return new Window(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            ownerId,
            name,
            appId,
            position,
            size);
    }

    public async Task ChangeNameAsync(Window window, string name, CancellationToken ct = default)
    {
        if (window.Name == name)
        {
            return;
        }

        await CheckNameAsync(name, ct);

        window.SetName(name);
    }

    public void ChangePosition(Window window, WindowPosition position)
    {
        if (window.Position.X == position.X && window.Position.Y == position.Y)
        {
            return;
        }

        window.Position = position;
    }

    public void ChangeSize(Window window, WindowSize size)
    {
        if (window.Size.Height == size.Height && window.Size.Width == size.Width)
        {
            return;
        }

        window.Size = size;
    }

    private async Task CheckNameAsync(string name, CancellationToken ct)
    {
        if (await repository.AnyAsync(x => x.Name == name, ct))
        {
            throw new WindowNameAlreadyExistsException(name);
        }
    }
}
