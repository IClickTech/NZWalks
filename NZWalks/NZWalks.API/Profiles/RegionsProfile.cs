﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, RegionAdd>().ReverseMap();
            CreateMap<Region, RegionUpdate>().ReverseMap();
        }
    }
}
