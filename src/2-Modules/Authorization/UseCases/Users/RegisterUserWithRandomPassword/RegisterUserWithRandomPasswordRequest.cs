using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Attributes;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.RegisterUserWithRandomPassword;

public class RegisterUserWithRandomPasswordRequest : BaseCommand
{
    [Display(Name = nameof(AuthorizationResource.User_Email), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    [ValidateEmail(ErrorMessageResourceName = nameof(CoreResource.Invalid), ErrorMessageResourceType = typeof(CoreResource))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
