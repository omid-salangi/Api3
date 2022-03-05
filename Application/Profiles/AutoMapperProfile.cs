using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;
using AutoMapper;
using Domain.Model;

namespace Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<Userdto, User>().ForMember(
                des => des.PasswordHash,
                opt => opt.Ignore()
            )
                .ReverseMap()
                .ForMember(
                    des => des.Password,
                    opt => opt.Ignore()
                    );
            //.ForMember(
            //    dest => dest.FullName,
            //    opt => opt.MapFrom(src => $"{src.FullName}")
            //)
            //.ForMember(
            //    dest => dest.Email,
            //    opt => opt.MapFrom(src => $"{src.Email}")
            //)
            //.ForMember(
            //    dest => dest.age,
            //    opt => opt.MapFrom(src => $"{src.Age}")
            //).ForMember(
            //    dest => dest.UserName,
            //    opt => opt.MapFrom(src => $"{src.UserName}")
            //).ReverseMap();
            this.CreateMap<Postdto, Post>().ReverseMap();
        }
    }
}
