using DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Post> Posts { get; }
        IRepository<Thread> Threads { get; }
        IRepository<Topic> Topics { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Notification> Notifications { get; }

        Task SaveChangesAsync();
    }
}
