using DAL.Domain;
using DAL.Infrastructure;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ForumDbContext _context;

        public UnitOfWork(ForumDbContext context, IRepository<Post> postRepository,
            IRepository<Thread> threadRepository, IRepository<Notification> notificationRepository,
            IRepository<UserProfile> userRepository, IRepository<Topic> topicRepository)
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

        public IRepository<Post> Posts { get; }
        public IRepository<Thread> Threads { get; }
        public IRepository<Topic> Topics { get; }
        public IRepository<UserProfile> UserProfiles { get; }
        public IRepository<Notification> Notifications { get; }
    }
}
