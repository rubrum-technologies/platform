using Google.Protobuf;
using Grpc.Core;
using Rubrum.Platform.BlobStorageService.Grpc;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobGrpcService(
    BlobManager manager,
    IBlobRepository repository) : BlobGrpc.BlobGrpcBase
{
    public override async Task<BlobByIdResponse> GetById(BlobByIdRequest request, ServerCallContext context)
    {
        var ct = context.CancellationToken;
        var id = Guid.Parse(request.Id);
        var blob = await repository.GetAsync(id, true, ct);

        var content = await manager.GetFileAsync(blob, ct);

        return new BlobByIdResponse
        {
            Id = id.ToString(),
            File = await ByteString.FromStreamAsync(content.GetStream(), ct),
        };
    }
}
