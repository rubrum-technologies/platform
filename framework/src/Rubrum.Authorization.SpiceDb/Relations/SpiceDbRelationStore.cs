using Authzed.Api.V1;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace Rubrum.Authorization.Relations;

[Dependency(ReplaceServices = true)]
public class SpiceDbRelationStore(
    ICurrentUser currentUser,
    PermissionsService.PermissionsServiceClient permissionsClient) : IRelationStore, ITransientDependency
{
    public async Task<bool> IsGrantedAsync(
        PermissionLink permission,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default)
    {
        var result = await permissionsClient.CheckPermissionAsync(
            new CheckPermissionRequest
            {
                Permission = permission.Name.ToSnakeCase(),
                Resource = new Authzed.Api.V1.ObjectReference
                {
                    ObjectType = resource.Type.ToSnakeCase(),
                    ObjectId = resource.Id,
                },
                Subject = new Authzed.Api.V1.SubjectReference
                {
                    Object = new Authzed.Api.V1.ObjectReference
                    {
                        ObjectType = subject.Type.ToSnakeCase(),
                        ObjectId = subject.Id,
                    },
                },
                Consistency = new Consistency { FullyConsistent = true },
                WithTracing = true,
            },
            cancellationToken: ct);

        return result.Permissionship == CheckPermissionResponse.Types.Permissionship.HasPermission;
    }

    public Task<bool> IsGrantedWithUserAsync(
        PermissionLink permission,
        ResourceReference resource,
        CancellationToken ct = default)
    {
        return IsGrantedAsync(
            permission,
            resource,
            new SubjectReference("user", currentUser.GetId().ToString()),
            ct);
    }

    public async Task GiveGrantAsync(
        Relation relation,
        ResourceReference resource,
        SubjectReference subject,
        CancellationToken ct = default)
    {
        await permissionsClient.WriteRelationshipsAsync(
            new WriteRelationshipsRequest
            {
                Updates =
                {
                    new RelationshipUpdate
                    {
                        Operation = RelationshipUpdate.Types.Operation.Touch,
                        Relationship = new Relationship
                        {
                            Relation = relation.Name.ToSnakeCase(),
                            Resource = new Authzed.Api.V1.ObjectReference
                            {
                                ObjectType = resource.Type.ToSnakeCase(),
                                ObjectId = resource.Id,
                            },
                            Subject = new Authzed.Api.V1.SubjectReference
                            {
                                Object = new Authzed.Api.V1.ObjectReference
                                {
                                    ObjectType = subject.Type.ToSnakeCase(),
                                    ObjectId = subject.Id,
                                },
                            },
                        },
                    },
                },
            },
            cancellationToken: ct);
    }

    public Task GiveGrantWithUserAsync(
        Relation relation,
        ResourceReference resource,
        CancellationToken ct = default)
    {
        return GiveGrantAsync(
            relation,
            resource,
            new SubjectReference("user", currentUser.GetId().ToString()),
            ct);
    }
}
