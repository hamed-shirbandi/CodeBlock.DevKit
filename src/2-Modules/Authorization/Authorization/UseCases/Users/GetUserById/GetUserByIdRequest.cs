// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Application.Queries;
using CodeBlock.DevKit.Authorization.Dtos;

namespace CodeBlock.DevKit.Authorization.UseCases.Users.GetUserById;

public class GetUserByIdRequest : BaseQuery<GetUserDto>
{
    public GetUserByIdRequest(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}
