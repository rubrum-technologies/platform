using CookieCrumble;
using HotChocolate.Types.Relay;
using Rubrum.Graphql;
using Rubrum.Platform.BlobStorageService.Folders;
using Shouldly;
using Xunit;
using static Rubrum.Platform.BlobStorageService.BlobStorageServiceTestConstants;

namespace Rubrum.Platform.BlobStorageService;

public sealed class FolderBlobMutationsTests : RubrumGraphqlTestBase<BlobStorageServiceApplicationTestModule>
{
    private readonly INodeIdSerializer _idSerializer;

    public FolderBlobMutationsTests()
    {
        _idSerializer = GetRequiredService<INodeIdSerializer>();
    }

    [Fact]
    public async Task ChangeName_Suscess()
    {
        var folderId = _idSerializer.Format(nameof(FolderBlob), FolderBlobId);

        await using var result = await ExecuteRequestAsync(b => b.SetDocument(
            $$"""
               mutation {
                  changeFolderBlobName(input: {id: "{{folderId}}", name: "Test2"}) {
                      folderBlob {
                          id
                          name
                      }
                      errors {
                          __typename
                      }
                  }
               }
              """));

        result.ShouldNotBeNull();
        result.MatchSnapshot();
    }
}
