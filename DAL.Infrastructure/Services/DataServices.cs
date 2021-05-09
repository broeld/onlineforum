﻿using DAL.Domain;
using DAL.Infrastructure.Repositories;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Infrastructure.Services
{
    public static class DataServices
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<ForumDbContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ForumDbContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Notification>, NotificationRepository>();
            services.AddScoped<IRepository<Post>, PostRepository>();
            services.AddScoped<IRepository<Thread>, ThreadRepository>();
            services.AddScoped<IRepository<Topic>, TopicRepository>();
            services.AddScoped<IRepository<UserProfile>, UserRepository>();

            return services;
        }
    }
}
