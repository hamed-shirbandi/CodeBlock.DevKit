// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Authorization.Domain.Users;

public interface IPasswordService
{
    string CreatePasswordSalt(int size);
    string CreatePasswordHash(string password, string salt);
}

