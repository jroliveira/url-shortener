﻿using AutoMapper;
using UrlShortener.Entities;

namespace UrlShortener.WebApi.Lib.Mappings
{
    public class UrlProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Models.Url.Post.Url, Url>()
                .ForMember(d => d.CreationDate, o => o.Ignore())
                .ForMember(d => d.Deleted, o => o.Ignore())
                .ForMember(d => d.Shortened, o => o.Ignore());

            CreateMap<Url, Models.Url.Get.Url>();
        }
    }
}