using DAL.Domain;
using DAL.Infrastructure.Context;
using DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class UnitOfWork
    {
        private readonly ForumDbContext _context;

        public UnitOfWork(ForumDbContext context, IGenericRepository<Post> postRepository,
            IGenericRepository<Thread> threadRepository, IGenericRepository<Notification> notificationRepository,
            IGenericRepository<UserProfile> userRepository, IGenericRepository<Topic> topicRepository)
        {
            _context = context;
            Posts = postRepository;
            Threads = threadRepository;
            Notifications = notificationRepository;
            UserProfiles = userRepository;
            Topics = topicRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IGenericRepository<Post> Posts { get; }
        public IGenericRepository<Thread> Threads { get; }
        public IGenericRepository<Topic> Topics { get; }
        public IGenericRepository<UserProfile> UserProfiles { get; }
        public IGenericRepository<Notification> Notifications { get; }
    }
}
    
}
