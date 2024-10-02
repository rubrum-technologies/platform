using Volo.Abp.Domain.Entities;

namespace Rubrum.Authorization.Analyzers.Models;

public class Resource : Entity<Guid>
{
    public required string Name { get; set; }
}
