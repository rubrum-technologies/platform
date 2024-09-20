using Volo.Abp.Domain.Values;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobMetadata : ValueObject
{
    public required long Size { get; init; }

    public required string MimeType { get; init; }

    public required string Filename { get; init; }

    public required string Extension { get; init; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Size;
        yield return MimeType;
        yield return Filename;
        yield return Extension;
    }
}
