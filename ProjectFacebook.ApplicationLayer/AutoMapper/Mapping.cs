using AutoMapper;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, ExternalLoginDTO>().ReverseMap();
            CreateMap<AppUser, EditProfileDTO>().ReverseMap();
            CreateMap<AppUser, ProfileSummaryDTO>().ReverseMap();
            CreateMap<AppUser, SearchUserDTO>().ReverseMap();

            CreateMap<Follow, FollowDTO>().ReverseMap();


            CreateMap<Like, LikeDTO>().ReverseMap();


            CreateMap<Post, SendPostDTO>().ReverseMap();

            CreateMap<Comment, AddCommentDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(a => a.AppUser.UserName))
                .ForMember(x => x.UserImage, opt => opt.MapFrom(a => a.AppUser.ImagePath))
                .ReverseMap();



        }
    }
}
