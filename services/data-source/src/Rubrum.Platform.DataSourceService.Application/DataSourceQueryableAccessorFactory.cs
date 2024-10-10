using Rubrum.Platform.DataSourceService.Database;
using Rubrum.Platform.DataSourceService.Graphql;
using Rubrum.Platform.DataSourceService.Grpc;
using Rubrum.Platform.DataSourceService.OData;
using Rubrum.Platform.DataSourceService.OpenApi;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceQueryableAccessorFactory(
    IDataSourceAssemblyAccessorFactory assemblyAccessorFactory,
    IDatabaseSourceDataOptionsAccessor databaseSourceDataOptionsAccessor)
    : IDataSourceQueryableAccessorFactory, ITransientDependency
{
    public IDataSourceQueryableAccessor Get(DataSource dataSource)
    {
        return dataSource switch
        {
            DatabaseSource databaseSource =>
                new DatabaseSourceQueryableAccessor(
                    databaseSource,
                    databaseSourceDataOptionsAccessor,
                    assemblyAccessorFactory),
            GraphqlSource => throw new NotImplementedException(),
            GrpcSource => throw new NotImplementedException(),
            ODataSource => throw new NotImplementedException(),
            OpenApiSource => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(dataSource))
        };
    }
}
