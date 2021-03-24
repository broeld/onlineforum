using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class NotificationService : BaseService, INotificationService
    {
        public Task CreateAsync(NotificationDto notificationDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificationDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NotificationDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(NotificationDto notificationDto)
        {
            throw new NotImplementedException();
        }
    }
}
