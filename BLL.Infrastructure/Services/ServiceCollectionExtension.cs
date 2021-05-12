using AutoMapper;
using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure.Services
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IThreadService, ThreadService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
