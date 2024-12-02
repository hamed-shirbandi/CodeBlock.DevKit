// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Domain.ValueObjects;

public abstract record BaseValueObject
{
    protected abstract void CheckPolicies();
}

