using BLL.Infrastructure;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineForum.UnitTests.ServiceTests
{
    public class NotificationServiceTests
    {
        private IEnumerable<NotificationModel> GetTestNotificationModels()
        {
            return new List<NotificationModel>()
            {
                new NotificationModel {
                    Id = 10001,
                    PostId = 201,
                    UserProfileId = 15,
                    NotificationDate = DateTime.Now
                }
            };
        }

        private IEnumerable<Notification> GetTestNotificationEntities()
        {
            return new List<Notification>()
            {
                new Notification {
                    Id = 10001,
                    PostId = 201,
                    UserProfileId = 15,
                    NotificationDate = DateTime.Now
                }
            };
        }

        [Test]
        public async Task NotificationService_GetAll_ReturnsNotificationModels()
        {
            var expected = GetTestNotificationModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Notifications.GetAllAsync())
                .ReturnsAsync(GetTestNotificationEntities());

            var service = new NotificationService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var notifications = await service.GetAllAsync();
            var actual = notifications.ToList();

            for (int i=0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].PostId, actual[i].PostId);
                Assert.AreEqual(expected[i].UserProfileId, actual[i].UserProfileId);
            }
        }

        [Test]
        public async Task NotificationService_GetById_ReturnsSingleNotificationModel()
        {
            var expected = GetTestNotificationModels().First();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Notifications.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestNotificationEntities().First);
            var service = new NotificationService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var actual = await service.GetByIdAsync(10001);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.PostId, actual.PostId);
            Assert.AreEqual(expected.UserProfileId, actual.UserProfileId);

        }

        [Test]
        public async Task NotificationService_RemoveAsync_DeleteNotificationModelFromDatabase()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Notifications.Remove(It.IsAny<Notification>()));
            var service = new NotificationService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var notification = GetTestNotificationModels().First();
            
            await service.RemoveAsync(notification);

            mockUnitOfWork.Verify(x => x.Notifications.Remove(It.Is<Notification>(n => n.Id == notification.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task NotificationService_CreateAsync_CreateNotificationModel()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Notifications.CreateAsync(It.IsAny<Notification>()));
            var service = new NotificationService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var notification = new NotificationModel() { Id = 10015, PostId = 201, UserProfileId = 15 };

            await service.CreateAsync(notification);

            mockUnitOfWork.Verify(x => x.Notifications.CreateAsync(It.Is<Notification>(n => n.Id == notification.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
