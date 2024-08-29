namespace CodeBlock.DevKit.Web.Services.AuthenticatedUser;

public interface IAuthenticatedUserService
{
    bool IsAuthenticated();
    string GetUserId();
    string GetUserName();
    AuthenticatedUserModel GetAuthenticatedUser();
}
