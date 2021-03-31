using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationModel>> GetAllAsync();
        Task<NotificationModel> GetByIdAsync(int id);
        Task CreateAsync(NotificationModel notificationModel);
        Task RemoveAsync(NotificationModel notificationModel);
    }
}
