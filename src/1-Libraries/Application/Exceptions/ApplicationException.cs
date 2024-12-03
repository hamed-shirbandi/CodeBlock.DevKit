// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using CodeBlock.DevKit.Core.Exceptions;

namespace CodeBlock.DevKit.Application.Exceptions;

public class ApplicationException : ManagedException
{
    public ApplicationException(string messageResourceKey, Type messageResourceType, IEnumerable<MessagePlaceholder> messagePlaceholders = null)
        : base(messageResourceKey, messageResourceType, messagePlaceholders) { }

    public ApplicationException(string message)
        : base(message) { }
}
