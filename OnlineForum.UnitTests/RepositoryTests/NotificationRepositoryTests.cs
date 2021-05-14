using DAL.Domain;
using DAL.Infrastructure;
using DAL.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineForum.UnitTests.RepositoryTests
{
    [TestFixture]
    public class NotificationRepositoryTests
    {
        [Test]
        public async Task NotificationRepository_GetAll_ReturnsAllValuesAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new NotificationRepository(context);

                var notifications = await repo.GetAllAsync();

                Assert.AreEqual(1, notifications.Count());
            }
        }

        [Test]
        public async Task NotificationRepository_GetById_ReturnsSingleValueAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new NotificationRepository(context);

                var notification = await repo.GetByIdAsync(10001);

                Assert.AreEqual(10001, notification.Id);
                Assert.AreEqual(201, notification.PostId);
                Assert.AreEqual(15, notification.UserProfileId);
            }
        }

        [Test]
        public async Task NotificationRepository_CreateAsync_AddsValueToDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new NotificationRepository(context);
                var notification = new Notification() { Id = 10002, PostId = 201, UserProfileId = 15, NotificationDate=DateTime.Now };

                await repo.CreateAsync(notification);
                await context.SaveChangesAsync();

                Assert.AreEqual(2, context.Notifications.Count());
            }
        }

        [Test]
        public async Task NotificationRepository_DeleteAsyncDeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new NotificationRepository(context);
                var notification = await repo.GetByIdAsync(10001);

                repo.Remove(notification);
                await context.SaveChangesAsync();

                Assert.AreEqual(0, context.Notifications.Count());
            }
        }

        [Test]
        public async Task NotificationRepository_UpdateAsync_UpdateValueInDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new NotificationRepository(context);
                var notification = new Notification() { Id = 10001, PostId = 202, UserProfileId = 15, NotificationDate = DateTime.Now };

                repo.Update(notification);
                await context.SaveChangesAsync();

                var notificationUpdated = await repo.GetByIdAsync(10001);

                Assert.AreEqual(10001, notificationUpdated.Id);
                Assert.AreEqual(202, notificationUpdated.PostId);
                Assert.AreEqual(15, notificationUpdated.UserProfileId);
            }
        }
    }
}
