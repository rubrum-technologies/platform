namespace Rubrum.Auditing;

public interface IMustHaveOwner
{
    /// <summary>
    /// Id of the owner.
    /// </summary>
    Guid OwnerId { get; }
}
