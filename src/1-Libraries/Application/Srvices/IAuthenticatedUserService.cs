// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Application.Srvices;

public interface IAuthenticatedUserService
{
    bool IsAuthenticated();
    string GetUserId();
    string GetUserName();
    string GetEmail();
}

