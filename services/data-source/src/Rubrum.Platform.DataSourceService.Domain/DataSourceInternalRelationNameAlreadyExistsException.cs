using Volo.Abp;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceInternalRelationNameAlreadyExistsException(string relationName) : BusinessException
{
    public string RelationName => relationName;
}
