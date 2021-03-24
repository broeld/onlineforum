using DAL.Domain;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Post> Posts { get; }
        IGenericRepository<Thread> Threads { get; }
        IGenericRepository<Topic> Topics { get; }
        IGenericRepository<UserProfile> UserProfiles { get; }
        IGenericRepository<Notification> Notifications { get; }

        Task SaveChangesAsync();
    }
}
