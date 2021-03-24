using DAL.Infrastructure.Context;
using DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class UnitOfWork
    {
        private readonly ForumDbContext _context;

        public UnitOfWork(ForumDbContext context, IPostRepository postRepository,
            IThreadRepository threadRepository, INotificationRepository notificationRepository,
            IUserRepository userRepository, ITopicRepository topicRepository)
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

        public IPostRepository Posts { get; }
        public IThreadRepository Threads { get; }
        public ITopicRepository Topics { get; }
        public IUserRepository UserProfiles { get; }
        public INotificationRepository Notifications { get; }
    }
}
    
}
