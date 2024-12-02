using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Exceptions;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Exceptions;

namespace CodeBlock.DevKit.Authorization.Domain.Users;

internal static class UserExceptions
{
    public static DomainException EmailIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(AuthorizationResource.User_Email, typeof(AuthorizationResource)) }
        );
    }

    public static DomainException EmailIsNotValid()
    {
        return new DomainException(
            nameof(CoreResource.Invalid),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(AuthorizationResource.User_Email, typeof(AuthorizationResource)) }
        );
    }

    public static DomainException EmailMustBeUnique()
    {
        return new DomainException(
            nameof(CoreResource.ALready_Exists),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(AuthorizationResource.User_Email, typeof(AuthorizationResource)) }
        );
    }

    public static Exception PasswordIsRequired()
    {
        return new DomainException(
            nameof(CoreResource.Required),
            typeof(CoreResource),
            new List<MessagePlaceholder> { MessagePlaceholder.CreateResource(AuthorizationResource.User_Password, typeof(AuthorizationResource)) }
        );
    }
}
