using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.SetRandomUserPassword;

public class ChangeUserPasswordRequest : BaseCommand
{
    [Display(Name = nameof(AuthorizationResource.Email), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CommonResource.Required), ErrorMessageResourceType = typeof(CommonResource))]
    public string Email { get; set; }
}
