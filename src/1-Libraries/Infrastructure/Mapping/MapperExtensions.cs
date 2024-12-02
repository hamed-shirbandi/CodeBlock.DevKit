// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace CodeBlock.DevKit.Infrastructure.Mapping;

/// <summary>
///
/// </summary>
public static class MapperExtensions
{
    /// <summary>
    ///
    /// </summary>
    public static void AddMapper(this IServiceCollection services, Type mappingProfileAssemblyMarkerType)
    {
        if (mappingProfileAssemblyMarkerType == null)
            return;

        //this will find all profiles in mappingProfileAssemblyMarkerType assemply
        services.AddAutoMapper(mappingProfileAssemblyMarkerType);
        services.AddAutoMapper(typeof(CommonMappingProfile));
    }
}

