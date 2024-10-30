using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Authorization.Resources;

namespace CodeBlock.DevKit.Authorization.Dtos;

public class GetRoleDto
{
    public string Id { get; set; }

    [Display(Name = nameof(AuthorizationResource.Role_Name), ResourceType = typeof(AuthorizationResource))]
    public string Name { get; set; }

    [Display(Name = nameof(AuthorizationResource.Role_Users_Count), ResourceType = typeof(AuthorizationResource))]
    public long UsersCount { get; set; }
}
