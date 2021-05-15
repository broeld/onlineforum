using System;
using AutoMapper;
using BLL.Models;
using DAL.Domain;
using Web.ViewModels;

namespace Web.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //DAL and BLL
            CreateMap<RegistrationModel, ApplicationUser>();

            CreateMap<UserProfile, UserModel>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(up => up.ApplicationUser.UserName))
                .ForMember(d => d.Email, opt => opt.MapFrom(up => up.ApplicationUser.Email))
                .ReverseMap()
                .ForMember(d => d.Posts, opt => opt.Ignore())
                .ForMember(d => d.Threads, opt => opt.Ignore())
                .ForMember(d => d.Notifications, opt => opt.Ignore());

            CreateMap<Post, PostModel>()
                .ReverseMap()
                .ForMember(d => d.UserProfile, opt => opt.Ignore())
                .ForMember(d => d.Thread, opt => opt.Ignore())
                .ForMember(d => d.RepliedPost, opt => opt.Ignore());

            CreateMap<Thread, ThreadModel>().ReverseMap();
            CreateMap<Topic, TopicModel>().ReverseMap();
            CreateMap<Notification, NotificationModel>().ReverseMap();

            //BLL and Web
            CreateMap<UserModel, AuthorModel>();
            CreateMap<UserModel, UserViewModel>();

            CreateMap<NotificationModel, NotificationViewModel>()
                .ForMember(nvm => nvm.ThreadId, opt => opt.MapFrom(nm => nm.Post.ThreadId))
                .ReverseMap()
                .ForMember(nm => nm.NotificationDate, opt => opt.MapFrom(nvm => DateTime.Now));

            CreateMap<NotificationCreateModel, NotificationModel>()
                .ForMember(dest => dest.NotificationDate, opt => opt.MapFrom(cd => DateTime.Now));

            CreateMap<PostModel, PostDetailModel>();
            CreateMap<PostModel, PostReplyModel>();
            CreateMap<PostCreateModel, PostModel>()
                .ForMember(dest => dest.PostDate, opt => opt.MapFrom(cd => DateTime.Now));

            CreateMap<ThreadModel, ThreadDisplayViewModel>()
                .ForMember(dest => dest.PostsNumber, opt => opt.MapFrom(tm => tm.Posts.Count));

            CreateMap<ThreadCreateModel, ThreadModel>()
                .ForMember(dest => dest.ThreadOpenedDate, opt => opt.MapFrom(ctvm => DateTime.Now))
                .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(ctvm => true));

            CreateMap<TopicModel, TopicDetailModel>()
                .ForMember(dest => dest.NumberOfThreads, opt => opt.MapFrom(tdto => tdto.Threads.Count))
                .ReverseMap();

            CreateMap<TopicCreateModel, TopicModel>();
        }
    }
}
