using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Infrastructure;
using DAL.Infrastructure.Repositories;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.ViewModels;

namespace OnlineForum.UnitTests.ControllersTests
{
    [TestFixture]
    public class NotificationControllerTests
    {
        private Mock<INotificationService> _service;
        private NotificationController _controller;

        [SetUp]
        public void SetUp()
        {
            _service = new Mock<INotificationService>();
            _controller = new NotificationController(_service.Object, UnitTestsHelper.CreateMapperProfile());
        }

        [Test]
        public async Task NotificationControlles_GetAll_ReturnsNotifications()
        {
            _service
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<NotificationModel>());

            var notifications = await _controller.GetAll();

            Assert.IsInstanceOf<ActionResult<IEnumerable<NotificationViewModel>>>(notifications);
        }
    }
}
