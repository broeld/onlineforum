using AutoMapper;
using BLL.Infrastructure.Automapper;
using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutomapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IThreadService, ThreadService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
