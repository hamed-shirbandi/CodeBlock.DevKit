// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Application.Queries;

/// <summary>
/// to mark queries that we want to cache
/// </summary>
public interface ICacheableQuery
{
    /// <summary>
    /// To enable caching for a query, call EnableCaching() beafor sending the query:
    /// var query=new GetSomeQuery()
    /// query.EnableCaching();
    /// _requestDispatcher.SendQuery(query)
    /// </summary>
    void EnableCaching();
    bool CachingIsEnabled();
}

