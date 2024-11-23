namespace CodeBlock.DevKit.Application.Srvices;

public interface IAuthenticatedUserService
{
    bool IsAuthenticated();
    string GetUserId();
    string GetUserName();
    string GetEmail();
}
