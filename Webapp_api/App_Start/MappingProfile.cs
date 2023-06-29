using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webapp_api.Dtos;
using Webapp_api.Models;

namespace Webapp_api.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customers, CustomerDtos>();
            Mapper.CreateMap<CustomerDtos, Customers>().ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<Movies, MovieDtos>();
            Mapper.CreateMap<MovieDtos, Movies>().ForMember(m => m.Id, opt => opt.Ignore());

        }
    }
}