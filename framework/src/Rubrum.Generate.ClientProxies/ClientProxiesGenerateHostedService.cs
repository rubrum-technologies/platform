using CliWrap;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Rubrum.Generate;

public class ClientProxiesGenerateHostedService(
    IServer server,
    IHostApplicationLifetime hostApplicationLifetime,
    IOptions<RubrumGenerateClientProxiesOptions> options,
    ILogger<ClientProxiesGenerateHostedService> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Factory.StartNew(
            async () =>
            {
                try
                {
                    await Task.Delay(2000, cancellationToken);

                    await GenerateProxyAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                    throw;
                }
                finally
                {
                    hostApplicationLifetime.StopApplication();
                }
            },
            cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task GenerateProxyAsync(CancellationToken cancellationToken)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var address = server.Features.Get<IServerAddressesFeature>()?.Addresses.First();

        var proxyClients = options.Value.ProxyClients;

        foreach (var (module, path) in proxyClients)
        {
            // TODO: Нужно сделать более универсальное решение
            await Cli.Wrap("dotnet")
                .WithWorkingDirectory(Path.Combine(currentDirectory, "..", "..", path))
                .WithArguments($"abp generate-proxy -t csharp -url {address} --without-contracts -m {module}")
                .WithStandardOutputPipe(PipeTarget.ToDelegate(line => logger.LogInformation("{Line}", line)))
                .WithStandardErrorPipe(PipeTarget.ToDelegate(line => logger.LogInformation("{Line}", line)))
                .ExecuteAsync(cancellationToken);
        }
    }
}
