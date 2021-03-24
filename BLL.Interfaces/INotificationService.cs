using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetAllAsync();
        Task<NotificationDto> GetByIdAsync(int id);
        Task CreateAsync(NotificationDto notificationDto);
        Task RemoveAsync(NotificationDto notificationDto);
    }
}
