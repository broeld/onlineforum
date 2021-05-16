using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(ForumDbContext context) : base(context)
        {
        }

        public override IQueryable<Post> DbsetWithProperties()
        {
            return dbset.Include(p => p.RepliedPost)
                .Include(p => p.Replies)
                .Include(p => p.Thread)
                .Include(p => p.Notifications)
                .Include(p => p.UserProfile).ThenInclude(up => up.ApplicationUser).AsNoTracking();
        }
    }
}
