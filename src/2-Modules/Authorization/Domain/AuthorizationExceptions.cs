using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;

namespace CodeBlock.DevKit.Authorization.Domain;

public static class AuthorizationExceptions
{
    public static DomainException UserEmailIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new Dictionary<string, Type> { { AuthorizationResource.User_Email, typeof(AuthorizationResource) } }
        );
    }

    public static DomainException UserEmailMustBeUnique()
    {
        return new DomainException(
            nameof(CoreResource.ALready_Exists),
            typeof(CoreResource),
            new Dictionary<string, Type> { { AuthorizationResource.User_Email, typeof(AuthorizationResource) } }
        );
    }
}
