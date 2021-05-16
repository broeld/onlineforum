using DAL.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Infrastructure.Repositories
{
    public class UserRepository : Repository<UserProfile>
    {
        public UserRepository(ForumDbContext context) : base(context)
        {
        }

        public override IQueryable<UserProfile> DbsetWithProperties()
        {
            return dbset
                .Include(up => up.Threads)
                .Include(up => up.Posts)
                .Include(up => up.Notifications)
                .Include(up => up.ApplicationUser).AsNoTracking();
        }
    }
}
