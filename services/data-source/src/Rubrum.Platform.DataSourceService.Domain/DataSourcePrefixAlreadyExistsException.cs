using Volo.Abp;

namespace Rubrum.Platform.DataSourceService;

public class DataSourcePrefixAlreadyExistsException(string prefix) : BusinessException
{
    public string Prefix => prefix;
}
