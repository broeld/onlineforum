using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork unit;
        private IMapper mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper automapper)
        {
            unit = unitOfWork;
            mapper = automapper;
        }


        public async Task CreateAsync(NotificationModel notification)
        {
            if (notification == null)
            {
                return;
            }

            var notificationEntity = mapper.Map<NotificationModel, Notification>(notification);

            await unit.Notifications.CreateAsync(notificationEntity);
            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotificationModel>> GetAllAsync()
        {
            var notifications = await unit.Notifications.GetAllAsync();

            return mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationModel>>(notifications);

        }

        public async Task<NotificationModel> GetByIdAsync(int id)
        {
            var notification = await unit.Notifications.GetByIdAsync(id);
            if (notification != null)
            {
                return mapper.Map<Notification, NotificationModel>(notification);
            }

            return null;
        }

        public async Task RemoveAsync(NotificationModel notification)
        {
            if (notification == null)
            {
                return;
            }
            var notificationEntity = mapper.Map<NotificationModel, Notification>(notification);
            unit.Notifications.Remove(notificationEntity);

            await unit.SaveChangesAsync();
        }
    }
}
