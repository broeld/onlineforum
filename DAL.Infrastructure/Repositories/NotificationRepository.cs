using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Infrastructure.Repositories
{
    public class NotificationRepository : Repository<Notification>
    {
        public NotificationRepository(ForumDbContext context): base(context)
        {

        }

        public override IQueryable<Notification> DbsetWithProperties()
        {
            return dbset.Include(p => p.Post).ThenInclude(p => p.Thread)
                .Include(p => p.UserProfile).ThenInclude(up => up.ApplicationUser).AsNoTracking();
        }
    }
}
