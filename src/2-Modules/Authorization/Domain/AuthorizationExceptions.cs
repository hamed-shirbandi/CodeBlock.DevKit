using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.Domain;

public static class AuthorizationExceptions
{
    public static ManagedException UserEmailIsRequired()
    {
        return new ManagedException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new Dictionary<string, Type> { { AuthorizationResource.User_Email, typeof(AuthorizationResource) } }
        );
    }

    public static ManagedException UserEmailMustBeUnique()
    {
        return new ManagedException(
            nameof(CoreResource.ALready_Exists),
            typeof(CoreResource),
            new Dictionary<string, Type> { { AuthorizationResource.User_Email, typeof(AuthorizationResource) } }
        );
    }
}
