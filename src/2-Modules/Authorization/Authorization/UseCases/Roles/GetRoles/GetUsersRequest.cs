// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.UseCases.Roles.GetRoles;

public class GetRolesRequest : BaseQuery<IEnumerable<GetRoleDto>>
{
    public GetRolesRequest() { }
}

