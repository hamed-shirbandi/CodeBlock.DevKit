using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.VerifyUserPassword;

public class VerifyUserPasswordRequest : BaseQuery<GetUserDto>
{
    [Display(Name = nameof(AuthorizationResource.Email), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Email { get; set; }

    [Display(Name = nameof(AuthorizationResource.Password), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
