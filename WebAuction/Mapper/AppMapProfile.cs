using AutoMapper;
using Data.Entities.Identity;
using WebAuction.Models;

namespace WebAuction.Mapper
{
    public class AppMapProfile:Profile
    {
        public AppMapProfile() {
            CreateMap<RegisterViewModel, AppUser>()
                    .ForMember(x => x.Avatar, opt => opt.Ignore())
                    .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));
       
        CreateMap<AppUser,UserViewModel>()
                .ForMember(x=>x.Avatar, opt => opt.MapFrom(x=>$"/uploads/{x.Avatar}"))
                .ForMember(x=>x.Name,opt=>opt.MapFrom(x=>x.Name))
                .ForMember(x=>x.Email,opt=>opt.MapFrom(x=>x.Email));
        
        }


        
    }
}
