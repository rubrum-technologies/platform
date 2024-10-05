using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Definitions;
using LinqToDB.Infrastructure;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService.Database;

public class DatabaseSourceGraphqlModule(
    IServiceProvider serviceProvider) : ITypeModule, ISingletonDependency
{
    public event EventHandler<EventArgs>? TypesChanged;

    public async ValueTask<IReadOnlyCollection<ITypeSystemMember>> CreateTypesAsync(
        IDescriptorContext context,
        CancellationToken cancellationToken)
    {
        var repository = serviceProvider.GetRequiredService<IDatabaseSourceRepository>();
        var queryableManager = serviceProvider.GetRequiredService<IDatabaseSourceQueryableManager>();
        var graphqlTypes = new List<ITypeSystemMember>();

        var sources = await repository.GetListAsync(true, cancellationToken);

        foreach (var source in sources)
        {
            foreach (var table in source.Tables)
            {

            }
        }

        return graphqlTypes;
    }


}
