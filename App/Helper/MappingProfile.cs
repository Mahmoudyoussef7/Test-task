using App.DTO;
using AutoMapper;
using EFL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Profiler
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDTO>().ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.MaritalStatus.Status)).ReverseMap();
        }
    }
}
