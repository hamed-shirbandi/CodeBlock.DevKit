// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Domain.Specifications;

public interface ISpecification<TEntity>
{
    bool IsSatisfiedBy(TEntity entity);
}

