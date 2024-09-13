using Google.Protobuf;
using Grpc.Core;
using Rubrum.Platform.BlobStorageService.Grpc;

namespace Rubrum.Platform.BlobStorageService.Blobs;

public class BlobGrpcService(
    IBlobManager manager) : BlobGrpc.BlobGrpcBase
{
    public override async Task<BlobByIdResponse> GetById(BlobByIdRequest request, ServerCallContext context)
    {
        var ct = context.CancellationToken;
        var id = Guid.Parse(request.Id);

        var content = await manager.GetAsync(id, ct);

        return new BlobByIdResponse
        {
            Id = id.ToString(),
            File = await ByteString.FromStreamAsync(content.GetStream(), ct),
        };
    }
}
