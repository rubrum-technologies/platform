using FluentValidation;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Rubrum.Authorization.Relations;

namespace Rubrum.Platform.BlobStorageService.Folders.Commands;

[GraphQLName("ChangeFolderBlobNameInput")]
public sealed class ChangeFolderBlobNameCommand : IRequest<FolderBlob>
{
    [ID<FolderBlob>]
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public class Handler(
        FolderBlobManager manager,
        IFolderBlobRepository repository,
        IAuthorizationService authorization) : IRequestHandler<ChangeFolderBlobNameCommand, FolderBlob>
    {
        public async Task<FolderBlob> Handle(ChangeFolderBlobNameCommand request, CancellationToken cancellationToken)
        {
            await authorization.CheckAsync<FolderBlob>(FolderBlobDefinition.Edit, request.Id);

            var folder = await repository.GetAsync(request.Id, true, cancellationToken);

            await manager.ChangeNameAsync(folder, request.Name, cancellationToken);

            folder = await repository.UpdateAsync(folder, true, cancellationToken);

            return folder;
        }
    }

    public class Validator : AbstractValidator<ChangeFolderBlobNameCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(FolderBlobConstants.NameLength);
        }
    }
}
