using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.UpdateRole;

public class UpdateRoleRequest : BaseCommand
{
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Id { get; set; }

    [Display(Name = nameof(AuthorizationResource.Role_Name), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CoreResource.Required), ErrorMessageResourceType = typeof(CoreResource))]
    public string Name { get; set; }
}
