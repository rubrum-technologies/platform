using HotChocolate.Types;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Validation;

namespace Rubrum.Graphql.Errors;

public class AbpErrorFactory :
    IPayloadErrorFactory<BusinessException, BusinessError>,
    IPayloadErrorFactory<EntityNotFoundException, EntityNotFoundError>,
    IPayloadErrorFactory<AbpValidationException, ValidationError>,
    IPayloadErrorFactory<AbpAuthorizationException, AuthorizationError>
{
    public ValidationError CreateErrorFrom(AbpValidationException exception)
    {
        return new ValidationError(exception);
    }

    public BusinessError CreateErrorFrom(BusinessException exception)
    {
        return new BusinessError(exception);
    }

    public EntityNotFoundError CreateErrorFrom(EntityNotFoundException exception)
    {
        return new EntityNotFoundError(exception);
    }

    public AuthorizationError CreateErrorFrom(AbpAuthorizationException exception)
    {
        return new AuthorizationError(exception);
    }
}
