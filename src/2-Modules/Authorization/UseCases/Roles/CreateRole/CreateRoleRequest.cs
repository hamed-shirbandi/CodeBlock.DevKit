using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Application.Commands;
using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Core.Resources;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.CreateRole;

public class CreateRoleRequest : BaseCommand
{
    [Display(Name = nameof(AuthorizationResource.Role_Name), ResourceType = typeof(AuthorizationResource))]
    [Required(ErrorMessageResourceName = nameof(CommonResource.Required), ErrorMessageResourceType = typeof(CommonResource))]
    public string Name { get; set; }
}
