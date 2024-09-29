using FluentValidation;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;
using Rubrum.Authorization.Relations;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Rubrum.Platform.BlobStorageService.Folders.Commands;

[GraphQLName("CreateFolderBlobInput")]
public sealed class CreateFolderBlobCommand : IRequest<FolderBlob>
{
    [ID<FolderBlob>]
    public required Guid? ParentId { get; init; }

    public required string Name { get; init; }

    public sealed class Handler(
        ICurrentUser currentUser,
        IRelationStore relationStore,
        FolderBlobManager manager,
        IFolderBlobRepository repository) : IRequestHandler<CreateFolderBlobCommand, FolderBlob>, ITransientDependency
    {
        public async Task<FolderBlob> Handle(
            CreateFolderBlobCommand request,
            CancellationToken cancellationToken)
        {
            var folder = await manager.CreateAsync(
                currentUser.GetId(),
                request.ParentId,
                request.Name,
                cancellationToken);

            folder = await repository.InsertAsync(folder, true, cancellationToken);

            await relationStore.GiveGrantWithUserAsync<FolderBlob>(
                FolderBlobDefinition.Owner,
                folder.Id,
                cancellationToken);

            return folder;
        }
    }

    public sealed class Validator : AbstractValidator<CreateFolderBlobCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(FolderBlobConstants.NameLength);
        }
    }
}
