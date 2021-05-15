using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Domain;
using DAL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web;

namespace OnlineForum.IntegrationTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoveForumDbContextRegistration(services);

                var serviceProvider = GetInMemoryServiceProvider();

                services.AddDbContextPool<ForumDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.Empty.ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                    options.EnableSensitiveDataLogging();
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ForumDbContext>();

                SeedData(context);
            });
        }

        private static ServiceProvider GetInMemoryServiceProvider()
        {
            return new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        private static void RemoveForumDbContextRegistration(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ForumDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }

        private static void SeedData(ForumDbContext context)
        {
            string adminId = Guid.NewGuid().ToString();
            string roleId = Guid.NewGuid().ToString();

            context.Users.Add(new ApplicationUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "dinaiovcheva@gmail.com",
                NormalizedEmail = "DINAIOVCHEVA@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "admin",
                SecurityStamp = Guid.NewGuid().ToString()

            });
            context.UserProfiles.Add(new UserProfile
            {
                Id = 15,
                IsActive = true,
                Rating = 1000,
                RegistrationDate = DateTime.Now,
                ApplicationUserId = adminId
            });

            context.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });

            context.Topics.Add(new Topic
            {
                Id = 1001,
                Title = "Python",
                Description = "Python is the best programming language ever"
            });

            context.Topics.Add(new Topic
            {
                Id = 1002,
                Title = "PythonTwo",
                Description = "Python is the best programming language ever"
            });

            context.Threads.Add(new Thread
            {
                Id = 151,
                Title = "Test thread 1",
                Content = "Some content",
                IsOpen = true,
                ThreadOpenedDate = DateTime.Now,
                TopicId = 1001,
                UserProfileId = 15
            });
            context.Threads.Add(new Thread
            {
                Id = 152,
                Title = "Test thread two",
                Content = "Some content two",
                IsOpen = true,
                ThreadOpenedDate = DateTime.Now,
                TopicId = 1001,
                UserProfileId = 15
            });
            context.Posts.Add(new Post
            {
                Id = 201,
                PostDate = DateTime.Now,
                Content = "First reply to thread",
                UserProfileId = 15,
                ThreadId = 151
            });
            context.Posts.Add(new Post
            {
                Id = 202,
                PostDate = DateTime.Now,
                Content = "Reply to first reply to thread",
                UserProfileId = 15,
                ThreadId = 151,
                RepliedPostId = 201
            });
            context.Posts.Add(new Post
            {
                Id = 203,
                PostDate = DateTime.Now,
                Content = "Reply to second thread",
                UserProfileId = 15,
                ThreadId = 152,
            });
            context.Notifications.Add(new Notification
            {
                Id = 50,
                PostId = 201,
                UserProfileId = 15,
                NotificationDate = DateTime.Now
            });
            context.Notifications.Add(new Notification
            {
                Id = 52,
                PostId = 201,
                UserProfileId = 15,
                NotificationDate = DateTime.Now
            });

            context.SaveChanges();

        }
    }
}
