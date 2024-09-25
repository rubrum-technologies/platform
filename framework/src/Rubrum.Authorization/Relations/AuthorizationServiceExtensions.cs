using Microsoft.AspNetCore.Authorization;

namespace Rubrum.Authorization.Relations;

public static class AuthorizationServiceExtensions
{
    public static Task CheckAsync(
        this IAuthorizationService authorizationService,
        PermissionLink permission,
        ResourceReference resource,
        SubjectReference subject)
    {
        return authorizationService.CheckAsync(
            null!,
            new RelationRequirement
            {
                Permission = permission,
                Resource = resource,
                Subject = subject,
            });
    }

    public static Task CheckAsync<T>(
        this IAuthorizationService authorizationService,
        PermissionLink permission,
        string id)
    {
        return authorizationService.CheckAsync(
            null!,
            new RelationRequirement
            {
                Permission = permission,
                Resource = new ResourceReference(typeof(T).Name, id),
            });
    }

    public static Task CheckAsync<T>(
        this IAuthorizationService authorizationService,
        PermissionLink permission,
        int id)
    {
        return authorizationService.CheckAsync<T>(permission, id.ToString());
    }

    public static Task CheckAsync<T>(
        this IAuthorizationService authorizationService,
        PermissionLink permission,
        long id)
    {
        return authorizationService.CheckAsync<T>(permission, id.ToString());
    }

    public static Task CheckAsync<T>(
        this IAuthorizationService authorizationService,
        PermissionLink permission,
        Guid id)
    {
        return authorizationService.CheckAsync<T>(permission, id.ToString());
    }
}
