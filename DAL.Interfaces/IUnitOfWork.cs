using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        IThreadRepository Threads { get; }
        ITopicRepository Topics { get; }
        IUserRepository UserProfiles { get; }
        INotificationRepository Notifications { get; }

        Task SaveChangesAsync();
    }
}
