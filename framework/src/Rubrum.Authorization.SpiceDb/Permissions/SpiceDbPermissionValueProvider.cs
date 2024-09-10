using Authzed.Api.V1;
using Google.Protobuf.WellKnownTypes;
using Volo.Abp.DependencyInjection;

namespace Rubrum.Authorization.Permissions;

public class SpiceDbPermissionValueProvider(
    PermissionsService.PermissionsServiceClient permissionsClient) : IPermissionValueProvider, ITransientDependency
{
    public string Name => "SpiceDb";

    public async Task<PermissionGrantResult> GetResultAsync(
        Relationship relationship,
        IReadOnlyDictionary<string, object>? context = null,
        CancellationToken ct = default)
    {
        var result = await permissionsClient.CheckPermissionAsync(
            new CheckPermissionRequest
            {
                Permission = relationship.Relation,
                Resource = new Authzed.Api.V1.ObjectReference
                {
                    ObjectType = relationship.Resource.Type,
                    ObjectId = relationship.Resource.Id,
                },
                Subject = new Authzed.Api.V1.SubjectReference
                {
                    Object = new Authzed.Api.V1.ObjectReference
                    {
                        ObjectType = relationship.Subject.Type,
                        ObjectId = relationship.Subject.Id,
                    },
                },
                Consistency = new Consistency { FullyConsistent = true },
                Context = ToStruct(context ?? new Dictionary<string, object>()),
                WithTracing = true,
            },
            cancellationToken: ct);

        return result.Permissionship switch
        {
            CheckPermissionResponse.Types.Permissionship.Unspecified => PermissionGrantResult.Undefined,
            CheckPermissionResponse.Types.Permissionship.NoPermission => PermissionGrantResult.Prohibited,
            CheckPermissionResponse.Types.Permissionship.HasPermission => PermissionGrantResult.Granted,
            CheckPermissionResponse.Types.Permissionship.ConditionalPermission => PermissionGrantResult.Undefined,
            _ => PermissionGrantResult.Undefined,
        };
    }

    private static Struct ToStruct(IReadOnlyDictionary<string, object> context)
    {
        var ps = new Struct();
        foreach (var (key, value) in context)
        {
            var pValue = value switch
            {
                string s => new Value { StringValue = s },
                bool b => new Value { BoolValue = b },
                double d => new Value { NumberValue = d },
                int i => new Value { NumberValue = i },
                long l => new Value { NumberValue = l },
                uint u => new Value { NumberValue = u },
                _ => new Value { NullValue = NullValue.NullValue },
            };
            ps.Fields.Add(key, pValue);
        }

        return ps;
    }
}
