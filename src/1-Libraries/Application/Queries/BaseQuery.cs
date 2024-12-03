// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using MediatR;

namespace CodeBlock.DevKit.Application.Queries;

/// <summary>
/// Mark BaseQuery with ICacheableQuery to enable catching for queries by CachingBehavior.
/// </summary>
public abstract class BaseQuery<TQueryResult> : ICacheableQuery, IRequest<TQueryResult>
{
    protected BaseQuery()
    {
        //Ching is disabled by default
        EnableCache = false;
    }

    public bool EnableCache { get; private set; }

    public bool CachingIsEnabled()
    {
        return EnableCache;
    }

    public void EnableCaching()
    {
        EnableCache = true;
    }
}
