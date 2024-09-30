using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.RegisterUser;

public class RegisterUserRequest : BaseCommand
{
    [Display(Name = nameof(AuthorizationResource.Mobile), ResourceType = typeof(AuthorizationResource))]
    public string Mobile { get; set; }

    [Display(Name = nameof(AuthorizationResource.Email), ResourceType = typeof(AuthorizationResource))]
    public string Email { get; set; }

    [Display(Name = nameof(AuthorizationResource.Password), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CommonResource.Required), ErrorMessageResourceType = typeof(CommonResource))]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
