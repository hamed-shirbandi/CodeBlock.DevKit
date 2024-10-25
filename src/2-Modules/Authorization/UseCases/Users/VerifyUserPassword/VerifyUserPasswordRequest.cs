using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Authorization.Dtos;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.VerifyUserPassword;

public class VerifyUserPasswordRequest : BaseQuery<GetUserDto>
{
    [Display(Name = nameof(AuthorizationResource.EmailOrMobile), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CommonResource.Required), ErrorMessageResourceType = typeof(CommonResource))]
    public string EmailOrMobile { get; set; }

    [Display(Name = nameof(AuthorizationResource.Password), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CommonResource.Required), ErrorMessageResourceType = typeof(CommonResource))]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
