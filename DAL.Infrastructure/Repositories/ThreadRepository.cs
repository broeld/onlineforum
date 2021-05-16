using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Infrastructure.Repositories
{
    public class ThreadRepository : Repository<Thread>
    {
        public ThreadRepository(ForumDbContext context) : base(context)
        {
        }

        public override IQueryable<Thread> DbsetWithProperties()
        {
            return dbset
                .Include(t => t.Posts)
                .Include(t => t.Topic)
                .Include(t => t.UserProfile)
                .ThenInclude(up => up.ApplicationUser).AsNoTracking();
        }
    }
}
