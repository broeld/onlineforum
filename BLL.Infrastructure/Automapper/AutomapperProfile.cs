using AutoMapper;
using BLL.Models;
using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure.Automapper
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            // Registration
            CreateMap<RegistrationModel, ApplicationUser>();

            // User
            CreateMap<UserProfile, UserModel>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(up => up.ApplicationUser.UserName))
                .ForMember(d => d.Email, opt => opt.MapFrom(up => up.ApplicationUser.Email))
                .ReverseMap()
                .ForMember(d => d.Posts, opt => opt.Ignore())
                .ForMember(d => d.Threads, opt => opt.Ignore())
                .ForMember(d => d.Notifications, opt => opt.Ignore());

            // Post
            CreateMap<PostModel, Post>()
                .ReverseMap()
                .ForMember(d => d.UserProfile, opt => opt.Ignore())
                .ForMember(d => d.Thread, opt => opt.Ignore())
                .ForMember(d => d.RepliedPost, opt => opt.Ignore());

            CreateMap<ThreadModel, Thread>().ReverseMap();
            CreateMap<TopicModel, Topic>().ReverseMap();
            CreateMap<Notification, NotificationModel>().ReverseMap();
        }
    }
}
