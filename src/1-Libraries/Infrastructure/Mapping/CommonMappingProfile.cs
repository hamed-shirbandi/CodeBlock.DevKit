// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using AutoMapper;

namespace CodeBlock.DevKit.Infrastructure.Mapping;

public class CommonMappingProfile : Profile
{
    public CommonMappingProfile()
    {
        //CreateMap<CreationTime, CreationTimeDto>()
        //    .ForMember(dest => dest.CreateDateTimeString, opt => opt.MapFrom(src => src.CreateDateTime.ToLongDateString()))
        //    .ForMember(dest => dest.ModifiedDateTimeString, opt => opt.MapFrom(src => src.ModifiedDateTime.ToLongDateString()));
    }
}
