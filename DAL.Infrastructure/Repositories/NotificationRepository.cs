using DAL.Domain;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure.Repositories.Generic
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
    }
}
