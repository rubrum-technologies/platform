using Volo.Abp.Domain.Entities;

namespace Rubrum.Authorization.Analyzers.Models;

public class Organization : Entity<Guid>
{
    public required string Name { get; set; }
}
