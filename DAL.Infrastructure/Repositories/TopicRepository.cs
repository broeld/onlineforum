using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Infrastructure.Repositories
{
    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(ForumDbContext context) : base(context)
        {
        }

        public override IQueryable<Topic> DbsetWithProperties()
        {
            return dbset
                .Include(p => p.Threads)
                .ThenInclude(t => t.UserProfile)
                .ThenInclude(up => up.ApplicationUser).AsNoTracking();
        }
    }
}
