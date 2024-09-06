using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

[ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
public interface IMyProjectNameDbContext : IEfCoreDbContext;
