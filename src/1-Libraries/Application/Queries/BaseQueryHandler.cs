// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;

namespace CodeBlock.DevKit.Application.Queries;

/// <summary>
///
/// </summary>
public abstract class BaseQueryHandler
{
    #region Fields

    protected readonly IMapper _mapper;

    #endregion


    #region Ctors


    protected BaseQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    #endregion


    #region Protected Methods



    #endregion
}

