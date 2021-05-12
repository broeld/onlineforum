using DAL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    public class ForumDbContext: IdentityDbContext<ApplicationUser>
    {
        public ForumDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Seed Data
            string adminId = Guid.NewGuid().ToString();
            string roleId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "dinaiovcheva@gmail.com",
                NormalizedEmail = "DINAIOVCHEVA@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "admin"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId,
            });

            builder.Entity<UserProfile>().HasData(new UserProfile
            {
                Id = 1,
                IsActive = true,
                Rating = 1000,
                RegistrationDate = DateTime.Now,
                ApplicationUserId = adminId
            });

            builder.Entity<Topic>().HasData(new Topic
            {
                Id = 101,
                Title = "Python",
                Description = "Python is the best programming language ever"
            });

            builder.Entity<Thread>().HasData(
                new Thread
                {
                    Id = 1,
                    Title = "Test thread 1",
                    Content = "Some content",
                    IsOpen = true,
                    ThreadOpenedDate = DateTime.Now,
                    TopicId = 101,
                    UserProfileId = 1
                },
                new Thread
                {
                    Id = 2,
                    Title = "Test thread two",
                    Content = "Some content two",
                    IsOpen = true,
                    ThreadOpenedDate = DateTime.Now,
                    TopicId = 101,
                    UserProfileId = 1
                }
            );

            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    PostDate = DateTime.Now,
                    Content = "First reply to thread",
                    UserProfileId = 1,
                    ThreadId = 1
                },

                new Post
                {
                    Id = 2,
                    PostDate = DateTime.Now,
                    Content = "Reply to first reply to thread",
                    UserProfileId = 1,
                    ThreadId = 1,
                    RepliedPostId = 1
                },

                new Post
                {
                    Id = 3,
                    PostDate = DateTime.Now,
                    Content = "Reply to second thread",
                    UserProfileId = 1,
                    ThreadId = 2,
                }
                );

            #endregion

            base.OnModelCreating(builder);
        }
    }
}
